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

    private bool interactingWithPlayer = false;
    private bool shootingCanonBall = false;
    [SerializeField] private float rotatedAngle = 0;

    private float rotateSpeed = 0.25f;
    private readonly float maxRotation = 15f;
    private readonly float shootForce = 100f;
    private readonly float shootDuration = 5f;
    private readonly int orangeDamage = 1;
    private readonly int greenDamage = 2;

    private Vector2 canonBallPosition;

    private bool InsideRedOfIndicator => rotatedAngle > 5 || rotatedAngle < -5;
    private bool InsideOrangeOfIndicator => (rotatedAngle > 3 && rotatedAngle <= 5) || (rotatedAngle < -3 && rotatedAngle >= -5);

    private Timer shootTimer;

    public event Action<int> OnCanonBallShot;

    private void Awake()
    {
        canonBallPosition = canonball.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) ActivateCanon();

        if(!interactingWithPlayer)
            return;

        RotateCanon();
        CheckForCanonShot();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {         
            if (InsideRedOfIndicator)
            {
                Debug.Log("insideRed");
                ShootCanon(0);
            }
            else if (InsideOrangeOfIndicator)
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
        canonball.GetComponent<Rigidbody2D>().AddForce(barrelTF.right * shootForce);
        indicator.SetActive(false);
        barrelRay.SetActive(false);
        shootTimer = new Timer(shootDuration, () => OnFinishedShooting(damage));        
        shootingCanonBall = true;
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
        Debug.Log($"finished shooting with damage {damage}");
    }

    private void ActivateCanon()
    {
        interactingWithPlayer = true;
        indicator.SetActive(true);
        barrelRay.SetActive(true);
    }

    private void RotateCanon()
    {      
        rotatedAngle += rotateSpeed;

        if (rotatedAngle >= maxRotation || rotatedAngle <= -maxRotation)
        {
            rotateSpeed *= -1;          
        }

        barrelTF.RotateAround(pivotTF.transform.position, Vector3.forward, rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            interactingWithPlayer = true;
        }
    }
}
