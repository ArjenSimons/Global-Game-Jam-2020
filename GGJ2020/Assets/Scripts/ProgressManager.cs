using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProgressManager
{
    [SerializeField] private int raceDistance;
    [SerializeField] private BoatMovement boat1;
    [SerializeField] private BoatMovement boat2;

    public float getProgression(BoatType boot)
    {
        int distanceCovered = 0;
        switch (boot)
        {
            case BoatType.BOAT_ONE:
                distanceCovered = boat1.distanceCovered;
                break;
            case BoatType.BOAT_TWO:
                distanceCovered = boat2.distanceCovered;
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        float progression = distanceCovered / raceDistance * 100f;

        return progression;
    }
}
