﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public enum Player
    {
        One,
        Two
    }

    [Header("Health of each player")]
    [SerializeField] BoatHealth playerOneHealth;
    [SerializeField] BoatHealth playerTwoHealth;

    [Header("Boat segments of each player")]
    [SerializeField] private List<BoatSegment> playerOneBoat;
    [SerializeField] private List<BoatSegment> playerTwoBoat;

    [Header("Damage values")]
    [SerializeField] private int smallDamage;
    [SerializeField] private int bigDamage;

    private const int MAX_SEGMENTS = 8;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ShootCannon(Player.Two);
        }
    }

    // Shoot at a players' boat
    public void ShootCannon(Player target)
    {
        int randomDamage = Random.Range(0, 2) == 0 ? smallDamage : bigDamage;

        switch (target)
        {
            case Player.One:
                playerOneHealth.DamageBoat(randomDamage);
                DamageBoatSegment(Player.One, randomDamage);
                break;
            case Player.Two:
                playerTwoHealth.DamageBoat(randomDamage);
                DamageBoatSegment(Player.Two, randomDamage);
                break;
        }
    }

    // Damage a segment of the players' boat
    private void DamageBoatSegment(Player player, int damageDealt)
    {
        int randomSegment = Random.Range(0, MAX_SEGMENTS);

        switch (player)
        {
            case Player.One:
                if (playerOneBoat[randomSegment].MyStatus == BoatSegment.Status.NoDamage)
                {
                    playerOneBoat[randomSegment].DamageBoatSegment(damageDealt == smallDamage ?
                        BoatSegment.Status.SmallDamage : BoatSegment.Status.BigDamage);
                    return;
                }
                break;
            case Player.Two:
                if (playerTwoBoat[randomSegment].MyStatus == BoatSegment.Status.NoDamage)
                {
                    playerTwoBoat[randomSegment].DamageBoatSegment(damageDealt == smallDamage ?
                        BoatSegment.Status.SmallDamage : BoatSegment.Status.BigDamage);
                    return;
                }
                break;
        }

        if (BoatIsHealthy(player))
        {
            DamageBoatSegment(player, damageDealt);
        }
    }

    // Checks if all boat segments are harmed
    private bool BoatIsHealthy(Player player)
    {
        switch (player)
        {
            case Player.One:
                foreach (BoatSegment segment in playerOneBoat)
                {
                    if (segment.MyStatus == BoatSegment.Status.NoDamage)
                    {
                        return true;
                    }
                }
                break;
            case Player.Two:
                foreach (BoatSegment segment in playerTwoBoat)
                {
                    if (segment.MyStatus == BoatSegment.Status.NoDamage)
                    {
                        return true;
                    }
                }
                break;
        }

        return false;
    }
}