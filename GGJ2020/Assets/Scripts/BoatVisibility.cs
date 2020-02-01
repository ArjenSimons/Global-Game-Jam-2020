using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatVisibility : MonoBehaviour
{
    [SerializeField]
    private ProgressManager progressManager;

    [SerializeField]
    private GameObject enemyBoat, fakeEnemyBoat;

    [SerializeField]
    private Camera cam;

    private Vector3 fakeBoatPos;

    private bool player1;

    private float completeCamView, startPosCameraView, percentageEqualToPlayer, percentage, fakeBoatPosX, playerProgression, enemyProgression;

    [SerializeField]
    private float visibilityRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        completeCamView = cam.farClipPlane / 23.25f;
        startPosCameraView = transform.position.x - (completeCamView / 2);
        if (gameObject.name == "Boat1")
        {
            player1 = true;
        }
        else
        {
            player1 = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkBoatDistance();
    }

    private void checkBoatDistance()
    {
        if (player1)
        {
            playerProgression = progressManager.getProgression(Player.PLAYER_TWO);
            enemyProgression = progressManager.getProgression(Player.PLAYER_ONE);
        } else
        {
            playerProgression = progressManager.getProgression(Player.PLAYER_ONE);
            enemyProgression = progressManager.getProgression(Player.PLAYER_TWO);
        }

        if (enemyProgression > playerProgression - visibilityRange && enemyProgression < playerProgression + visibilityRange)
        {
            if (!fakeEnemyBoat.activeSelf)
            {
                fakeEnemyBoat.SetActive(true);
            }
            showBoat();
        } else
        {
            if (fakeEnemyBoat.activeSelf)
            {
                fakeEnemyBoat.SetActive(false);
            }
        }
    }

    private void showBoat()
    {
        percentageEqualToPlayer = visibilityRange + (enemyProgression - playerProgression);
        percentage = percentageEqualToPlayer / (visibilityRange * 2);
        fakeBoatPosX = completeCamView * percentage;

        fakeBoatPos = fakeEnemyBoat.transform.position;
        fakeBoatPos.x = startPosCameraView + fakeBoatPosX;
        fakeEnemyBoat.transform.position = fakeBoatPos;
    }
}
