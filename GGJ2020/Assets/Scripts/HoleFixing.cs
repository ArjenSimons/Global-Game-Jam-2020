﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class detects player that collides with a damaged boat segment
/// </summary>
public class HoleFixing : MonoBehaviour
{
    public GameObject player;
    public GameObject repairButtonIndicator;
    private bool mayPressBtnA;

    private const string PLAYER_TAG = "Player";

    private Player playerEnum;

    private BoatSegment boatSegment;

    private SequencePuzzleP1 puzzlePlayer1;
    private SequencePuzzleP2 puzzlePlayer2;

    private void Start()
    {
        boatSegment = GetComponent<BoatSegment>();

        puzzlePlayer1 = player.GetComponent<SequencePuzzleP1>();

        puzzlePlayer2 = player.GetComponent<SequencePuzzleP2>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && mayPressBtnA)
        {
            Debug.Log("XD");
            DisplaySequencePuzzle();
        }
    }

    /// <summary>
    /// tries to detect holes in all the boat parts
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG &&
            (boatSegment.MyStatus == BoatSegment.Status.SmallDamage ||
            boatSegment.MyStatus == BoatSegment.Status.BigDamage))
        {
            ShowButtonIndicator();
            mayPressBtnA = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG &&
            (boatSegment.MyStatus == BoatSegment.Status.SmallDamage ||
            boatSegment.MyStatus == BoatSegment.Status.BigDamage))
        {
            HideButtonIndicator();
            mayPressBtnA = false;
        }
    }

    // Shows the activate repair button
    private void ShowButtonIndicator()
    {
        repairButtonIndicator.SetActive(true);
    }

    // Hides the activate repair button
    private void HideButtonIndicator()
    {
        repairButtonIndicator.SetActive(false);
    }

    // method do GENERATE SEQUENCE PUZZLE when pressed A BUTTON
    private void DisplaySequencePuzzle()
    {
        HideButtonIndicator();
        mayPressBtnA = false;

        Debug.Log("Je ziet nu de puzzel");

        if (playerEnum == Player.PLAYER_ONE)
        {
            //puzzlePlayer1.enabled = true;
            Debug.Log("yo xD");
        }
        else if (playerEnum == Player.PLAYER_TWO)
        {
            //puzzlePlayer2.enabled = true;

            Debug.Log("player 2 bby xD");
        }
    }
}
