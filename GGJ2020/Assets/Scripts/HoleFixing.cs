using System.Collections;
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

    private float inputDownPlayer1;
    private float inputDownPlayer2;

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
        inputDownPlayer1 = Input.GetAxis("DownJoystickVertical1");
        inputDownPlayer2 = Input.GetAxis("DownJoystickVertical2");

        if (tryingToActivate)
        {
            switch (playerEnum)
            {
                case Player.PLAYER_ONE:
                    if (inputDownPlayer2 <= -0.2 && mayPressBtnA)
                    {
                        Invoke("StartSequencePuzzle", 0.5f);
                        tryingToActivate = false;
                    }
                    break;
                case Player.PLAYER_TWO:
                    if (inputDownPlayer1 <= -0.2 && mayPressBtnA)
                    {
                        Invoke("StartSequencePuzzle", 0.5f);
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
            tryingToActivate = true;
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

    public void ResetBtnA()
    {
        mayPressBtnA = true;
    }

    // method do GENERATE SEQUENCE PUZZLE when pressed A BUTTON
    private void StartSequencePuzzle()
    {
        Debug.Log("Started sequence.");
        HideButtonIndicator();
        mayPressBtnA = false;

        if (playerEnum == Player.PLAYER_ONE)
        {
            puzzlePlayer1.RetrieveBoatSegment(boatSegment);
            puzzlePlayer1.StartSequencePuzzle();
        }
        else if (playerEnum == Player.PLAYER_TWO)
        {
            puzzlePlayer2.RetrieveBoatSegment(boatSegment);
            puzzlePlayer2.StartSequencePuzzle();
        }
    }
}
