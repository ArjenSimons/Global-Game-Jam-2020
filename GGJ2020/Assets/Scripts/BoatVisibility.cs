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

    private Player boatType, enemyBoatType;

    private Vector3 fakeBoatPos;

    private float completeCamView, startPosCameraView, percentageEqualToPlayer, percentage, fakeBoatPosX, playerProgression, enemyProgression;

    [SerializeField]
    private float visibilityRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        completeCamView = cam.farClipPlane / 23.25f;
        boatType = GetComponent<Boat>().PlayerType;
        enemyBoatType = enemyBoat.GetComponent<Boat>().PlayerType;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkBoatDistance();
    }

    private void checkBoatDistance()
    {
        playerProgression = progressManager.getProgression(boatType);
        enemyProgression = progressManager.getProgression(enemyBoatType);

        if (enemyProgression > playerProgression - visibilityRange && enemyProgression < playerProgression + visibilityRange)
        {
            showBoat(enemyProgression, playerProgression);
        } 
    }

    private void showBoat(float enemyPos, float playerPos)
    {
        startPosCameraView = transform.position.x - (completeCamView / 2);
        percentageEqualToPlayer = visibilityRange + (enemyPos - playerPos);
        percentage = percentageEqualToPlayer / (visibilityRange * 2);
        fakeBoatPosX = completeCamView * percentage;

        fakeBoatPos = fakeEnemyBoat.transform.position;
        fakeBoatPos.x = startPosCameraView + fakeBoatPosX;
        fakeEnemyBoat.transform.position = fakeBoatPos;
    }
}
