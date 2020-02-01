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

        ProgressManager.onPlayerFinish.AddListener(setfinish);
    }

    private void FixedUpdate()
    {
        calculateSpeed();

        speed = Mathf.Clamp(speed, float.MinValue, maxSpeed);

        distanceCovered += speed;

        
    }

    private void setfinish(Player player)
    {
        Debug.Log(player + ": finished");
    }

    private void calculateSpeed()
    {
        //Map to value between zero and one
        float speedMultiplier = Math.Normalize(boatHealth.health, boatHealth.minHealth, boatHealth.maxHealth);

        float adjustedSpeed = maxSpeed * speedMultiplier;

        //Map adjustedSpeed to value between the minSpeed and the maxSpeed
        speed = (maxSpeed - minSpeed) * adjustedSpeed / maxSpeed + minSpeed;
    }
}
