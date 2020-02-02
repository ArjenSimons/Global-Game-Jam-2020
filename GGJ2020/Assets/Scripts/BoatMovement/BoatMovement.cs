using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private int minSpeed;
    [SerializeField] private int _maxSpeed;
    [SerializeField] private int boostAmount = 30;
    [SerializeField] private int boostTime = 5;

    [SerializeField] private ProgressManager progressionManager;

    public int maxSpeed { get { return _maxSpeed; } }
    private BoatHealth boatHealth;

    public float speed { get; private set; }
    public float distanceCovered { get; private set; }

    private bool boostIsActive = false;

    private GameObject[] clouds1, clouds2;

    private float timer;

    public bool paused;

    [SerializeField]
    private AudioManager audioManager;

    private void Start()
    {
        boatHealth = GetComponent<BoatHealth>();
        boatHealth.onHealthChanged.AddListener(SetSpeed);
        speed = _maxSpeed;

        clouds1 = GameObject.FindGameObjectsWithTag(Tags.Cloud1.ToString());
        clouds2 = GameObject.FindGameObjectsWithTag(Tags.Cloud2.ToString());
    }

    private void FixedUpdate()
    {
        // speed = Mathf.Clamp(speed, float.MinValue, _maxSpeed);

        if (!paused)
        {
            distanceCovered += speed * Time.deltaTime;
        }
        

        if (boostIsActive)
        {
            timer += Time.deltaTime;

            if (timer > boostTime)
            {
                timer = 0;
                boostIsActive = false;
                StartCoroutine(ChangeSpeedOverTime(CalculateSpeedDesiredSpeed(boatHealth.health)));
            }
        }
    }

    private void SetSpeed(Player player, int health, bool healthIncreased)
    {

        //if (!healthIncreased) //If getting hit instantly decrease speed
        //{
        //    if (progressionManager.IsAhead(player))
        //        speed = CalculateSpeedDesiredSpeed(health);
        //    else
        //        StartCoroutine(ChangeSpeedOverTime(CalculateSpeedDesiredSpeed(health)));
        //}
        //else
        //{
        //    if (progressionManager.IsAhead(player))
        //        StartCoroutine(ChangeSpeedOverTime(CalculateSpeedDesiredSpeed(health)));
        //    else
        //        speed = CalculateSpeedDesiredSpeed(health);
        //}
        if (!progressionManager.IsAhead(player) && healthIncreased)
        {
            //audioManager.Play("speedboost");
            boostIsActive = true;
        }

        if (boostIsActive)
        {
            health += boostAmount;

            if (health > 90)
                health = 90;
        }
        
        StopCoroutine("ChangeSpeedOverTime");
        StartCoroutine(ChangeSpeedOverTime(CalculateSpeedDesiredSpeed(health)));

        UpdateClouds();
    }

    private float CalculateSpeedDesiredSpeed(int health)
    {
        //Map to value between zero and one
        float speedMultiplier = Math.Normalize(health, boatHealth.minHealth, boatHealth.maxHealth);

        float adjustedSpeed = _maxSpeed * speedMultiplier;

        //Map adjustedSpeed to value between the minSpeed and the maxSpeed
        float newSpeed = (_maxSpeed - minSpeed) * adjustedSpeed / _maxSpeed + minSpeed;

        return newSpeed;
    }

    private IEnumerator ChangeSpeedOverTime(float desiredSpeed)
    {
        float speedIncreaseAmount = desiredSpeed  - speed;
        
        while (desiredSpeed > speed ? speedIncreaseAmount > 0 : speedIncreaseAmount < 0)
        {
            speed += speedIncreaseAmount / 20;
            UpdateClouds();
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void UpdateClouds() {

        if (this.gameObject.name == "Boat1")
        {
            foreach (GameObject cloud in clouds1)
            {
                cloud.GetComponent<CloudMovement>().ChangeSpeed(speed);
            }
        }
        else if (this.gameObject.name == "Boat2")
        {
            foreach (GameObject cloud in clouds2)
            {
                cloud.GetComponent<CloudMovement>().ChangeSpeed(speed);
            }
        }
    }
}
