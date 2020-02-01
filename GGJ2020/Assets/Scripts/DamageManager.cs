using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Health of each player")]
    [SerializeField] BoatHealth playerOneHealth;
    [SerializeField] BoatHealth playerTwoHealth;

    [Header("Boat segments of each player")]
    [SerializeField] private List<BoatSegment> playerOneBoat;
    [SerializeField] private List<BoatSegment> playerTwoBoat;

    //damage values
    public const int SMALLDAMAGE = 5;
    public const int BIGDAMAGE = 10;

    private const int MAX_SEGMENTS = 6;

    private void Awake()
    {
        Canon[] canons = FindObjectsOfType<Canon>();
        foreach(Canon canon in canons)
        {
            canon.OnCanonBallShot += DamagePlayer;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    DamagePlayer(Player.Two);
        //}
    }

    // Shoot at a players' boat
    public void DamagePlayer(Player target, int damage)
    {
        if (damage == 0)
            return;
        
        switch (target)
        {
            case Player.PLAYER_ONE:
                playerOneHealth.DamageBoat(damage);
                DamageBoatSegment(Player.PLAYER_ONE, damage);
                break;
            case Player.PLAYER_TWO:
                playerTwoHealth.DamageBoat(damage);
                DamageBoatSegment(Player.PLAYER_TWO, damage);
                break;
        }
    }

    // Damage a segment of the players' boat
    private void DamageBoatSegment(Player player, int damageDealt)
    {       
        int randomSegment = Random.Range(0, MAX_SEGMENTS);

        switch (player)
        {
            case Player.PLAYER_ONE:
                if (playerOneBoat[randomSegment].MyStatus == BoatSegment.Status.NoDamage)
                {
                    playerOneBoat[randomSegment].DamageBoatSegment(damageDealt == SMALLDAMAGE ?
                        BoatSegment.Status.SmallDamage : BoatSegment.Status.BigDamage);
                    return;
                }
                break;
            case Player.PLAYER_TWO:
                if (playerTwoBoat[randomSegment].MyStatus == BoatSegment.Status.NoDamage)
                {
                    playerTwoBoat[randomSegment].DamageBoatSegment(damageDealt == SMALLDAMAGE ?
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
            case Player.PLAYER_ONE:
                foreach (BoatSegment segment in playerOneBoat)
                {
                    if (segment.MyStatus == BoatSegment.Status.NoDamage)
                    {
                        return true;
                    }
                }
                break;
            case Player.PLAYER_TWO:
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
