using System;
using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private Transform barrelTF;
    [SerializeField] private Transform pivotTF;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject canonball;
    [SerializeField] private GameObject barrelRay;

    private bool interactingWithPlayer = true;
    private bool shootingCanonBall = false;
    private bool activated = false;
    private string playerInteracting;
    [SerializeField] private float rotatedAngle = 0;

    private float rotateSpeed = 0.35f;
    private float maxRotation;
    private float minRotation;
    private float startRotatedAngle;

    private readonly float rotateOffset = 15f;
    private readonly float shootForce = 100f;
    private readonly float shootDuration = 5f;
    private readonly int orangeDamage = 1;
    private readonly float minRotatedAngleForRedOffset = 6;
    private readonly float minRotatedAngleForOrangeOffset = 3;
    private readonly int greenDamage = 2;
    private float indicatorXOffset;
    private readonly float barrelYOffset = 0.15f;
    private readonly float leftOrientationRotateAngle = 180f;
    private readonly float middleOrientationRotateAngle = 90f;

    private Vector2 canonBallPosition;
    private Vector2 shootDirection;

    private Timer shootTimer;

    public event Action<int> OnCanonBallShot;

    [SerializeField] private Orientation orientation;
    private enum Orientation { LEFT, RIGHT, MIDDLE }

    private void Awake()
    {       
        SetOrientation();   
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
                shootDirection = barrelTF.right;             
                break;
            case Orientation.RIGHT:
                indicator.transform.localPosition = new Vector2(indicatorXOffset, indicator.transform.localPosition.y);               
                canonball.transform.localPosition = new Vector2(1, canonball.transform.localPosition.y);                             
                barrelTF.localPosition = new Vector2(barrelTF.localPosition.x, barrelYOffset);
                maxRotation = barrelTF.localEulerAngles.z + rotateOffset;
                minRotation = barrelTF.localEulerAngles.z - rotateOffset;
                shootDirection = barrelTF.right;
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
        print(maxRotation + " " + minRotation);
        canonBallPosition = canonball.transform.position;
    }

    private void Update()
    {
        if (PlayerInput() && interactingWithPlayer && !activated)
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

    private bool InsideRedOfIndicator()
    {
        print($"{startRotatedAngle + minRotatedAngleForRedOffset} {startRotatedAngle - minRotatedAngleForRedOffset}");
        print($"greater than {startRotatedAngle + minRotatedAngleForOrangeOffset} smaller or equal to {startRotatedAngle + minRotatedAngleForRedOffset}");
        print($"greater than {startRotatedAngle - minRotatedAngleForRedOffset} smaller or equal to {startRotatedAngle - minRotatedAngleForOrangeOffset}");
        
        print(rotatedAngle);
        return rotatedAngle > startRotatedAngle + minRotatedAngleForRedOffset || rotatedAngle < startRotatedAngle - minRotatedAngleForRedOffset;
    }

    private bool InsideOrangeOfIndicator()
    {
        return (rotatedAngle > startRotatedAngle + minRotatedAngleForOrangeOffset && rotatedAngle <= startRotatedAngle + minRotatedAngleForRedOffset)
        || (rotatedAngle >= startRotatedAngle - minRotatedAngleForRedOffset && rotatedAngle < startRotatedAngle - minRotatedAngleForOrangeOffset);
    }

    private bool PlayerInput()
    {       
        switch (playerInteracting)
        {
            case "Player1": return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("A-Button1");
            case "Player2": return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("A-Button2");
            default: return Input.GetButtonDown("A-Button2");              
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
        //uses main camera for now -> should use camera for his side of screen
        Vector3 screenPos = Camera.main.WorldToScreenPoint(canonball.transform.position);
        float halfWidth = canonball.GetComponent<SpriteRenderer>().sprite.rect.width * 0.5f;
        float halfHeight = canonball.GetComponent<SpriteRenderer>().sprite.rect.height * 0.5f;
        if (screenPos.x - halfWidth > Camera.main.pixelWidth || screenPos.x + halfWidth < 0
        || screenPos.y - halfHeight > Camera.main.pixelHeight || screenPos.y + halfHeight < 0)
        {
            OnFinishedShooting(damage);
        }
        
    }

    private void CheckForCanonShot()
    {
        if (PlayerInput())
        {         
            if (InsideRedOfIndicator())
            {
                Debug.Log("insideRed");
                ShootCanon(0);
            }
            else if (InsideOrangeOfIndicator())
            {
                Debug.Log("insideOrange");
                ShootCanon(orangeDamage);
            }
            else
            {
                Debug.Log("insideGreen");
                ShootCanon(greenDamage);
            }
        }
    }

    private void ShootCanon(int damage)
    {
        canonball.SetActive(true);
        canonball.GetComponent<Rigidbody2D>().AddForce(shootDirection * shootForce);
        indicator.SetActive(false);
        barrelRay.SetActive(false);
        shootTimer = new Timer(shootDuration, () => OnFinishedShooting(damage));        
        shootingCanonBall = true;
        activated = false;
        interactingWithPlayer = false;
        StartCoroutine(WaitForShotFinish(damage));
    }

    private void OnFinishedShooting(int damage)
    {
        Rigidbody2D ballRB = canonball.GetComponent<Rigidbody2D>();
        ballRB.velocity = Vector3.zero;
        ballRB.angularVelocity = 0;
        canonball.transform.position = canonBallPosition;
        canonball.SetActive(false);
        OnCanonBallShot(damage);
        shootingCanonBall = false;
        shootTimer = null;
        Debug.Log($"finished shooting with damage {damage}");
    }

    private void ActivateCanon()
    {     
        activated = true;
        indicator.SetActive(true);
        barrelRay.SetActive(true);
    }

    private void RotateCanon()
    {      
        rotatedAngle += rotateSpeed;

        if (rotatedAngle >= maxRotation || rotatedAngle <= minRotation)
        {
            rotateSpeed *= -1;          
        }

        barrelTF.RotateAround(pivotTF.transform.position, Vector3.forward, rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInteracting = collision.name;
            interactingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInteracting = "";
            interactingWithPlayer = false;
        }
    }
}
