using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProgressManager
{
    [SerializeField] private int raceDistance;
    [SerializeField] private Boot boot1;
    [SerializeField] private Boot boot2;



    public float getProgression(BootType boot)
    {
        int distanceCovered = 0;
        switch (boot)
        {
            case BootType.BOOT_ONE:
                distanceCovered = boot1.distanceCovered;
                break;
            case BootType.BOOT_TWO:
                distanceCovered = boot2.distanceCovered;
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        float progression = distanceCovered / raceDistance * 100f;

        return progression;
    }
}
