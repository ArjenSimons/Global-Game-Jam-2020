using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math 
{
    public static float Normalize(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}
