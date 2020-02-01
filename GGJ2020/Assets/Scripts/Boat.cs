using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private Player _PlayerType;
    [SerializeField] public Player PlayerType { get; private set; }
}
    