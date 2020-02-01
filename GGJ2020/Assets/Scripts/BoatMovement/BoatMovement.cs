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

    private GameObject[] clouds;

    private void Start()
    {
        boatHealth = GetComponent<BoatHealth>();
        speed = _maxSpeed;

        clouds = GameObject.FindGameObjectsWithTag(Tags.Cloud.ToString());
    }

    private void FixedUpdate()
    {
        calculateSpeed();

        speed = Mathf.Clamp(speed, float.MinValue, _maxSpeed);

        distanceCovered += speed;
    }

    private void calculateSpeed()
    {
        //Map to value between zero and one
        float speedMultiplier = Math.Normalize(boatHealth.health, boatHealth.minHealth, boatHealth.maxHealth);

        float adjustedSpeed = _maxSpeed * speedMultiplier;

        //Map adjustedSpeed to value between the minSpeed and the maxSpeed
        speed = (_maxSpeed - minSpeed) * adjustedSpeed / _maxSpeed + minSpeed;

        foreach (GameObject cloud in clouds)
        {
            cloud.GetComponent<CloudMovement>().ChangeSpeed(speed);
        }
    }
}
