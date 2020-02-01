using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private int minSpeed;
    [SerializeField] private int maxSpeed;
    private BoatHealth boatHealth;

    private float speed;
    public float distanceCovered { get; private set; }

    private void Start()
    {
        boatHealth = GetComponent<BoatHealth>();
        speed = maxSpeed;
    }

    private void FixedUpdate()
    {
        calculateSpeed();

        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        distanceCovered += speed;
    }

    private void calculateSpeed()
    {
        int dummyHealth = 80;
        int dummyMaxHealth = 80;
        int dummyMinHealth = 0;

        //Map to value between zero and one
        float speedMultiplier = Math.Normalize(boatHealth.health, boatHealth.minHealth, boatHealth.maxHealth);

        float adjustedSpeed = maxSpeed * speedMultiplier;

        Debug.Log(speedMultiplier);
        Debug.Log("Speed1: " + speed);
        //Map adjustedSpeed between the minSpeed and the maxSpeed
        speed = (maxSpeed - minSpeed) * (adjustedSpeed - minSpeed) / (maxSpeed - minSpeed) + minSpeed;

        Debug.Log("Speed2: " + speed);
    }
}
