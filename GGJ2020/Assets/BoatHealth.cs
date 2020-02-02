using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthChangeEvent : UnityEvent<Player, int, bool> { }

public class BoatHealth : MonoBehaviour
{
    //Don't access this variable access health instead!!!
    private int _health;

    [SerializeField]
    private Camera cam;

    public int health
    {
        get { return _health; }
        set
        {
            onHealthChanged.Invoke(playerType, value, value > _health);
            _health = value;
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
        if (health - damage <= minHealth)
        {
            health = minHealth;
        }
        else
        {
            health -= damage;
        }
        cam.GetComponent<ScreenShake>().activateScreenShake();
    }

    // Restore the damage dealt to the boat
    public void RestoreBoat(int restore)
    {
        health += restore;
    }
}
