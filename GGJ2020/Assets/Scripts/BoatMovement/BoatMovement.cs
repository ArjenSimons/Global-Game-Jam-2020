using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private int minSpeed;
    [SerializeField] private int _maxSpeed;

    public int maxSpeed { get { return _maxSpeed; } }
    private BoatHealth boatHealth;

    private float speed;
    public float distanceCovered { get; private set; }

    private GameObject[] clouds1, clouds2;

    private void Start()
    {
        boatHealth = GetComponent<BoatHealth>();
        speed = _maxSpeed;

        clouds1 = GameObject.FindGameObjectsWithTag(Tags.Cloud1.ToString());
        clouds2 = GameObject.FindGameObjectsWithTag(Tags.Cloud2.ToString());

        ProgressManager.onPlayerFinish.AddListener(setfinish);
    }

    private void FixedUpdate()
    {
        calculateSpeed();

        speed = Mathf.Clamp(speed, float.MinValue, _maxSpeed);

        distanceCovered += speed * Time.deltaTime;

        
    }

    private void setfinish(Player player)
    {
        Debug.Log(player + ": finished");
    }

    private void calculateSpeed()
    {
        //Map to value between zero and one
        float speedMultiplier = Math.Normalize(boatHealth.health, boatHealth.minHealth, boatHealth.maxHealth);

        float adjustedSpeed = _maxSpeed * speedMultiplier;

        //Map adjustedSpeed to value between the minSpeed and the maxSpeed
        speed = (_maxSpeed - minSpeed) * adjustedSpeed / _maxSpeed + minSpeed;

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
