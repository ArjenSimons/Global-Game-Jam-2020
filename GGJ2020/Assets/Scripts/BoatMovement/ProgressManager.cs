using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProgressManager
{
    [SerializeField] private int raceDistance;
    [SerializeField] private BoatMovement boat1;
    [SerializeField] private BoatMovement boat2;

    public static float getProgression(Player boot)
    {
        float distanceCovered = 0;
        switch (boot)
        {
            case Player.PLAYER_ONE:
                distanceCovered = boat1.distanceCovered;
                break;
            case Player.PLAYER_TWO:
                distanceCovered = boat2.distanceCovered;
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        float progression = distanceCovered / raceDistance * 100f;

        return progression;
    }
}
