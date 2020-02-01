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

    public Player playerEnum;

    public bool tryingToActivate;

    private BoatSegment boatSegment;

    private SequencePuzzleP1 puzzlePlayer1;
    private SequencePuzzleP2 puzzlePlayer2;

    private void Start()
    {
        boatSegment = GetComponent<BoatSegment>();

        puzzlePlayer1 = player.GetComponent<SequencePuzzleP1>();
        puzzlePlayer2 = player.GetComponent<SequencePuzzleP2>();

        tryingToActivate = true;
    }

    private void Update()
    {
        if (tryingToActivate)
        {
            switch (playerEnum)
            {
                case Player.PLAYER_ONE:
                    if (Input.GetButtonDown("A-Button1") && mayPressBtnA)
                    {
                        DisplaySequencePuzzle();
                        tryingToActivate = false;
                    }
                    break;
                case Player.PLAYER_TWO:
                    if (Input.GetButtonDown("B-Button1") && mayPressBtnA)
                    {
                        DisplaySequencePuzzle();
                        tryingToActivate = false;
                    }
                    break;
            }
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
        Debug.Log("Je ziet nu de puzzel");

        HideButtonIndicator();
        mayPressBtnA = false;

        if (playerEnum == Player.PLAYER_ONE)
        {
            puzzlePlayer1.RetrieveBoatSegment(boatSegment);
            puzzlePlayer1.SequenceRandomizer();
            puzzlePlayer1.isActivated = true;
        }
        else if (playerEnum == Player.PLAYER_TWO)
        {
            puzzlePlayer2.RetrieveBoatSegment(boatSegment);
            puzzlePlayer2.SequenceRandomizer();

            puzzlePlayer2.playerCanvas.gameObject.SetActive(true);
            Debug.Log("player 2 bby xD");
            puzzlePlayer2.isActivated = true;
        }
    }
}
