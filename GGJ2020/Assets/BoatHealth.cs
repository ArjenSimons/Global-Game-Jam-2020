using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHealth : MonoBehaviour
{
    public int health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;

    public int maxHealth { get { return _maxHealth; } }
    public int minHealth { get { return _minHealth; } }

    private void Start()
    {
        health = _maxHealth;
    }

    // Deal damage to the boat
    public void DamageBoat(int damage)
    {
        if (health - damage <= minHealth)
        {
            health = minHealth;
        }
        else
        {
            health -= damage;
        }
    }

    // Restore the damage dealt to the boat
    public void RestoreBoat(int restore)
    {
        health += restore;
    }
}
