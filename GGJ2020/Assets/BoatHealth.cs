using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthChangeEvent : UnityEvent<Player, int> { }

public class BoatHealth : MonoBehaviour
{
    //Don;t acces this variable acces health instead!!!
    private int _health;

    public int health
    {
        get { return _health; }
        set
        {
            _health = value;
            onHealthChanged.Invoke(playerType, health);
        }
    }

    public HealthChangeEvent onHealthChanged = new HealthChangeEvent();
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;

    public int maxHealth { get { return _maxHealth; } }
    public int minHealth { get { return _minHealth; } }

    private Player playerType;

    private void Start()
    {
        _health = _maxHealth;
        playerType = GetComponent<Boat>().PlayerType;
    }

    // Deal damage to the boat
    public void DamageBoat(int damage)
    {
        if (_health - damage <= minHealth)
        {
            _health = minHealth;
        }
        else
        {
            _health -= damage;
        }
    }

    // Restore the damage dealt to the boat
    public void RestoreBoat(int restore)
    {
        _health += restore;
    }
}
