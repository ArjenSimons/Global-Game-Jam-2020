using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private int minSpeed;
    [SerializeField] private int maxSpeed;

    private int speed;
    public int distanceCovered { get; private set; }

}
