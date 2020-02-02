using System;
using System.Collections;
using UnityEngine;
using Status = BoatSegment.Status;

public class Canon : MonoBehaviour
{
    [SerializeField] private Transform barrelTF;
    [SerializeField] private Transform pivotTF;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject canonball;
    [SerializeField] private GameObject barrelRay;
    [SerializeField] private BoatSegment segmentOn;
    [SerializeField] private bool debug;

    private bool interactingWithPlayer = false;
    private bool shootingCanonBall = false;
    private bool activated = false;
    [SerializeField] private PlayerMovementBase playerInteracting;
    [SerializeField] private float rotatedAngle = 0;

    private float currentRotateSpeed;
    private float rotateSpeed = 1.25f;
    private float maxRotation;
    private float minRotation;
    private float startRotatedAngle;
    private float extraAngleBasedOnOpponent;
    private float indicatorXOffset;

    private readonly float rotateOffset = 20f;
    private readonly float shootForce = 250f;
    private readonly float shootDuration = 3f;
    private readonly float minRotatedAngleForRedOffset = 5;
    private float extraRotateSpeed = 0.75f;
    private readonly float minRotatedAngleForOrangeOffset = 5;
    private readonly float barrelYOffset = 0.15f;
    private readonly float minProgressDifference = 10f;
    private readonly float maxProgressDifference = 25f;
    private readonly float leftOrientationRotateAngle = 180f;
    private readonly float middleOrientationRotateAngle = 90f;

    private Vector3 canonBallPosition;
    private Vector3 shootDirection;
    private Vector3 barrelStartPosition;
    private Vector3 barrelStartRotation;
    private Vector3 indicatorStartPosition;
    private Vector3 indicatorStartRotation;

    private Timer shootTimer;
    private Camera cam;
    private ProgressManager progressManager;
    private Player opponent;  

    public event Action<Player, int> OnCanonBallShot;

    [SerializeField] private Orientation orientation;
    private enum Orientation { LEFT, RIGHT, MIDDLE }
    public enum HitStatus { STATUS_MISSED, STATUS_HIT }

    [SerializeField]
    private AudioManager audioManager;

    private void Awake()
    {
        if (debug) interactingWithPlayer = true;
        GetReferences();
        SetOrientation();      
    }

    private void GetReferences()
    {
        Camera[] cams = FindObjectsOfType<Camera>();
        char boatNum = transform.root.name[transform.root.name.Length - 1];
        for(int i = 0; i < cams.Length; i++)
        {
            char camNum = cams[i].name[cams[i].name.Length - 1];
            if (char.IsDigit(camNum) && camNum == boatNum)
            {
                cam = cams[i];
                break;
            }
        }
        progressManager = FindObjectOfType<ProgressManager>();
    }

    private void SetOrientation()
    {
        indicatorXOffset = indicator.transform.localPosition.x;
        switch (orientation)
        {
            case Orientation.LEFT:               
                indicator.transform.localPosition = new Vector2(-indicatorXOffset, 0);
                indicator.transform.eulerAngles = new Vector3(0, 0, leftOrientationRotateAngle);
                canonball.transform.localPosition = new Vector2(-1, canonball.transform.localPosition.y);
                barrelTF.RotateAround(pivotTF.position, Vector3.forward, leftOrientationRotateAngle);
                barrelTF.localPosition = new Vector2(barrelTF.localPosition.x, barrelYOffset);
                maxRotation = barrelTF.localEulerAngles.z + rotateOffset;
                minRotation = barrelTF.localEulerAngles.z - rotateOffset;
                rotatedAngle = leftOrientationRotateAngle;
                startRotatedAngle = leftOrientationRotateAngle;                            
                break;
            case Orientation.RIGHT:
                indicator.transform.localPosition = new Vector2(indicatorXOffset, indicator.transform.localPosition.y);               
                canonball.transform.localPosition = new Vector2(1, canonball.transform.localPosition.y);                             
                barrelTF.localPosition = new Vector2(barrelTF.localPosition.x, barrelYOffset);
                maxRotation = barrelTF.localEulerAngles.z + rotateOffset;
                minRotation = barrelTF.localEulerAngles.z - rotateOffset;
                break;
            case Orientation.MIDDLE:
                indicator.transform.localPosition = new Vector2(0, indicator.transform.localPosition.y);
                canonball.transform.localPosition = new Vector2(0, canonball.transform.localPosition.y);
                barrelTF.RotateAround(pivotTF.position, Vector3.forward, middleOrientationRotateAngle);
                barrelTF.localPosition = new Vector2(barrelTF.localPosition.x, barrelYOffset);
                maxRotation = barrelTF.localEulerAngles.z + rotateOffset;
                minRotation = barrelTF.localEulerAngles.z - rotateOffset;
                rotatedAngle = middleOrientationRotateAngle;
                startRotatedAngle = middleOrientationRotateAngle;
                break;
        }
        canonBallPosition = canonball.transform.position;
        barrelStartPosition = barrelTF.localPosition;
        barrelStartRotation = barrelTF.localEulerAngles;
        indicatorStartPosition = indicator.transform.localPosition;
        indicatorStartRotation = indicator.transform.localEulerAngles;
    }

    private void Update()
    {
        if (PlayerInput() && interactingWithPlayer && !activated && shootTimer == null
        && playerInteracting.CarryingCanonBall)
            StartCoroutine(StartCanonActivation());       

        if (activated)
        {           
            CheckForCanonShot();
        }
        
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            RotateCanon();
        }
    }

    private void RotateRelativeToOpponent()
    {
        Player player = opponent == Player.PLAYER_ONE ? Player.PLAYER_TWO : Player.PLAYER_ONE;
        float progressDiff = Mathf.Abs(progressManager.getProgressionDifference(player));
        float angle = orientation == Orientation.LEFT ? -rotateOffset : rotateOffset;
        progressDiff = Mathf.Clamp(progressDiff, 0, maxProgressDifference);
        
        angle *= progressDiff * 0.01f;
        if(progressDiff > minProgressDifference)
        {            
            angle += orientation == Orientation.LEFT ? -rotateOffset : rotateOffset;
            extraAngleBasedOnOpponent = angle;
            barrelTF.RotateAround(pivotTF.position, Vector3.forward, angle);
            indicator.transform.RotateAround(pivotTF.position, Vector3.forward, angle);
        }
        rotatedAngle = orientation == Orientation.LEFT ? leftOrientationRotateAngle + angle : angle;
        minRotation = rotatedAngle - rotateOffset;
        maxRotation = rotatedAngle + rotateOffset;      
    }

    private bool InsideRedOfIndicator()
    {
        return rotatedAngle > startRotatedAngle + minRotatedAngleForRedOffset + extraAngleBasedOnOpponent 
        || rotatedAngle < startRotatedAngle - minRotatedAngleForRedOffset + extraAngleBasedOnOpponent;
    }

    private bool InsideOrangeOfIndicator()
    {
        return (rotatedAngle > startRotatedAngle + minRotatedAngleForOrangeOffset + extraAngleBasedOnOpponent 
            && rotatedAngle <= startRotatedAngle + minRotatedAngleForRedOffset + extraAngleBasedOnOpponent)
        || (rotatedAngle >= startRotatedAngle - minRotatedAngleForRedOffset + extraAngleBasedOnOpponent 
            && rotatedAngle < startRotatedAngle - minRotatedAngleForOrangeOffset + extraAngleBasedOnOpponent);
    }

    private bool PlayerInput()
    {
        if (playerInteracting == null)
            return false;

        switch (playerInteracting.name)
        {
            case "Player1": return Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A-Button2");
            case "Player2": return Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A-Button1");
            default: return Input.GetKeyDown(KeyCode.Space);              
        }       
    }

    private IEnumerator WaitForShotFinish(int damage)
    {
        while (shootingCanonBall)
        {
            shootTimer.Tick(Time.deltaTime);
            CheckForOffScreenCanonball(damage);
            yield return null;
        }
    }

    private IEnumerator StartCanonActivation()
    {
        yield return null;
        ActivateCanon();
    }

    private void CheckForOffScreenCanonball(int damage)
    {        
        Vector3 screenPos = cam.WorldToScreenPoint(canonball.transform.position);
        float halfWidth = canonball.GetComponent<SpriteRenderer>().sprite.rect.width * 0.5f;
        float halfHeight = canonball.GetComponent<SpriteRenderer>().sprite.rect.height * 0.5f;
        float camTopBound = cam.name == "UpperCamera1" ? Screen.height : cam.pixelHeight;
        float camBottomBound = cam.name == "UpperCamera1" ? cam.pixelHeight : 0;
        //print(screenPos + " " + cam.pixelWidth + " " + cam.pixelHeight);
        if (screenPos.x - halfWidth > cam.pixelWidth || screenPos.x + halfWidth < 0
        || screenPos.y - halfHeight > camTopBound)
        {
            if (!GuarenteedMiss() && damage != 0 && screenPos.y + halfHeight > camBottomBound)
            {
                int axis = screenPos.y - halfHeight > camTopBound || screenPos.y + halfHeight < camBottomBound ? 1 : 0;
                StartCoroutine(CrashCannonBallOnOpponent(screenPos, (status) => OnFinishedShooting(damage, status), axis));
            }
            else
            {
                OnFinishedShooting(damage, HitStatus.STATUS_MISSED);
            }
            shootingCanonBall = false;
        }  
    }

    private IEnumerator CrashCannonBallOnOpponent(Vector3 offscreenPosition, Action<HitStatus> OnHit, int fromAxis)
    {
        Vector3 screenSpawnPos;
        if(fromAxis == 0)
        {
            float screenY = playerInteracting.name == "Player1" ? offscreenPosition.y - cam.pixelHeight : offscreenPosition.y + cam.pixelHeight;           
            screenSpawnPos = offscreenPosition.x < 0 ? new Vector3(cam.pixelWidth, screenY) : new Vector3(0, screenY);          
        }
        else
        {
            float screenY = playerInteracting.name == "Player1" ? cam.pixelHeight : Screen.height;
            screenSpawnPos = offscreenPosition.x < 0 ? new Vector3(offscreenPosition.x, screenY) : new Vector3(0, screenY);
        }
        float cameraOffset = playerInteracting.name == "Player1" ? -GameStateManager.Instance.cameraOffset : GameStateManager.Instance.cameraOffset;
        Vector3 worldPos = cam.ScreenToWorldPoint(screenSpawnPos);
        worldPos = new Vector3(worldPos.x,worldPos.y + (cameraOffset * 0.5f), 0);
        canonball.transform.position = worldPos;
        CanonBall ball = canonball.GetComponent<CanonBall>();
        ball.SetFallingOnOpponent(opponent);
        yield return new WaitUntil(() => ball.Hit);
        ball.ResetBall();
        OnHit(ball.status);
    }

    private void CheckForCanonShot()
    {
        if (PlayerInput())
        {         
            if (InsideRedOfIndicator())
            {
                print("inside Red");
                ShootCanon(0);
            }
            else if (InsideOrangeOfIndicator())
            {
                print("inside Orange");
                ShootCanon(DamageManager.SMALLDAMAGE);
            }
            else
            {
                print("inside Green");
                ShootCanon(DamageManager.BIGDAMAGE);
            }
        }
    }

    private void ShootCanon(int damage)
    {
        canonball.SetActive(true);
        shootDirection = barrelTF.right;
        canonball.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce);             
        indicator.SetActive(false);
        barrelRay.SetActive(false);
        shootTimer = new Timer(shootDuration, () => OnFinishedShooting(damage, HitStatus.STATUS_MISSED));        
        shootingCanonBall = true;
        audioManager.Play("cannonfire1");
        activated = false;
        interactingWithPlayer = false;
        RotateCannonBack();               
        StartCoroutine(WaitForShotFinish(damage));
    }

    private void RotateCannonBack()
    {
        barrelTF.localPosition = barrelStartPosition;
        barrelTF.localEulerAngles = barrelStartRotation;
        indicator.transform.localPosition = indicatorStartPosition;
        indicator.transform.localEulerAngles = indicatorStartRotation;
        extraAngleBasedOnOpponent = 0;
    }

    private void OnFinishedShooting(int damage, HitStatus status)
    {
        Rigidbody2D ballRB = canonball.GetComponent<Rigidbody2D>();
        ballRB.velocity = Vector3.zero;
        ballRB.angularVelocity = 0;
        canonball.transform.position = canonBallPosition;
        shootTimer = null;
        canonball.SetActive(false);      
        if (status == HitStatus.STATUS_HIT)
        {
            OnCanonBallShot(opponent, damage);
        }
        else
        {
            audioManager.Play("cannonmiss");
        }
    }

    private bool GuarenteedMiss()
    {
        Player player = opponent == Player.PLAYER_ONE ? Player.PLAYER_TWO : Player.PLAYER_ONE;
        float progressDiff = Mathf.Abs(progressManager.getProgressionDifference(player));
        
        return ( progressDiff > 5f &&
        ( orientation == Orientation.LEFT && (progressManager.getProgression(player) < progressManager.getProgression(opponent)))
        || (orientation == Orientation.RIGHT && (progressManager.getProgression(player) > progressManager.getProgression(opponent))));
    }

    private void ActivateCanon()
    {     
        activated = true;
        indicator.SetActive(true);
        barrelRay.SetActive(true);
        playerInteracting.LoseCanonBall();
        RotateRelativeToOpponent();
        //audioManager.Play("aimingsound");
    }

    private void RotateCanon()
    {
        Player player = opponent == Player.PLAYER_ONE ? Player.PLAYER_TWO : Player.PLAYER_ONE;
        float progressDiff = progressManager.getProgressionDifference(player) * 0.1f;
        currentRotateSpeed = progressDiff > 0 ? rotateSpeed + (extraRotateSpeed * progressDiff) : rotateSpeed;       
        rotatedAngle += currentRotateSpeed;
        
        if (rotatedAngle >= maxRotation || rotatedAngle <= minRotation)
        {
            rotateSpeed *= -1;
            extraRotateSpeed *= -1;
        }

        barrelTF.RotateAround(pivotTF.transform.position, Vector3.forward, currentRotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInteracting = collision.GetComponent<PlayerMovementBase>();
            opponent = collision.name == "Player1" ? Player.PLAYER_TWO : Player.PLAYER_ONE;
            interactingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {           
            interactingWithPlayer = false;
            if (activated)
            {
                indicator.SetActive(false);
                barrelRay.SetActive(false);
                playerInteracting.GiveCannonBall();
                RotateCannonBack();
                activated = false;              
            }
        }
    }
}
