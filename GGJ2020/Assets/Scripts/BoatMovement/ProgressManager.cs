using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    [Tooltip("The minimum amount of seconds the race can last")]
    [SerializeField] private int raceDistance;
    [SerializeField] private BoatMovement boatOne;
    [SerializeField] private BoatMovement boatTwo;

    public class OnPlayerFinishEvent : UnityEvent<Player> { }

    public OnPlayerFinishEvent onPlayerFinish = new OnPlayerFinishEvent();

    private void Start()
    {
        raceDistance *= 100;
    }

    public float getProgression(Player boot)
    {
        float distanceCovered = 0;
        switch (boot)
        {
            case Player.PLAYER_ONE:
                distanceCovered = boatOne.distanceCovered;
                break;
            case Player.PLAYER_TWO:
                distanceCovered = boatTwo.distanceCovered;
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        float progression = distanceCovered / raceDistance * 100f;

        //Debug.Log(boot + ": " + progression);

        if (progression >= 100)
            onPlayerFinish.Invoke(boot);

        return progression;
    }
}
