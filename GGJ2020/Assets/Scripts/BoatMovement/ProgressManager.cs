using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private static int raceDistance;
    [SerializeField] private BoatMovement boatOne;
    [SerializeField] private BoatMovement boatTwo;

    public class OnPlayterFinishEvent : UnityEvent<Player> { }

    public static OnPlayterFinishEvent onPlayerFinish = new OnPlayterFinishEvent();

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

        if (progression >= 100)
            onPlayerFinish.Invoke(boot);

        return progression;
    }
}
