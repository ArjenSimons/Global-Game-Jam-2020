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

    private BoatSegment boatSegment;

    private SequencePuzzleP1 puzzlePlayer1;
    private SequencePuzzleP2 puzzlePlayer2;

    [SerializeField] private playerMovement playerOne;
    [SerializeField] private player2Movement playerTwo;

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
                    if (Input.GetButtonDown("A-Button2") && mayPressBtnA)
                    {
                        if (!IsInvoking("StartSequencePuzzle"))
                        {
                            Invoke("StartSequencePuzzle", 0f);
                        }
                        tryingToActivate = false;
                    }
                    break;
                case Player.PLAYER_TWO:
                    if (Input.GetButtonDown("A-Button1") && mayPressBtnA)
                    {
                        if (!IsInvoking("StartSequencePuzzle"))
                        {
                            Invoke("StartSequencePuzzle", 0f);
                        }
                        tryingToActivate = false;
                    }
                    break;
            }
        }

    }

    /// <summary>
    /// tries to detect holes in all the boat parts
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG &&
            (boatSegment.MyStatus == BoatSegment.Status.SmallDamage ||
            boatSegment.MyStatus == BoatSegment.Status.BigDamage))
        {
            switch (playerEnum)
            {
                case Player.PLAYER_ONE:
                    if (!playerTwo.repairEventStarted)
                    {
                        ShowButtonIndicator();
                        tryingToActivate = true;
                        mayPressBtnA = true;
                    }
                    else
                    {
                        HideButtonIndicator();
                    }
                    break;
                case Player.PLAYER_TWO:
                    if (!playerOne.repairEventStarted)
                    {
                        ShowButtonIndicator();
                        tryingToActivate = true;
                        mayPressBtnA = true;
                    }
                    else
                    {
                        HideButtonIndicator();
                    }
                    break;
            }
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

            switch (playerEnum)
            {
                case Player.PLAYER_ONE:
                    puzzlePlayer1.NullBoatSegment();
                    break;
                case Player.PLAYER_TWO:
                    puzzlePlayer2.NullBoatSegment();
                    break;
            }
        }
    }

    // Shows the activate repair button
    public void ShowButtonIndicator()
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
        tryingToActivate = true;
    }

    // method do GENERATE SEQUENCE PUZZLE when pressed A BUTTON
    private void StartSequencePuzzle()
    {
        Debug.Log("Started sequence.");
        HideButtonIndicator();
        mayPressBtnA = false;

        if (playerEnum == Player.PLAYER_ONE)
        {
            playerTwo.repairEventStarted = true;
            puzzlePlayer1.IntroduceNewHole(this);
            puzzlePlayer1.RetrieveBoatSegment(boatSegment);
            puzzlePlayer1.StartSequencePuzzle();
        }
        else if (playerEnum == Player.PLAYER_TWO)
        {
            playerOne.repairEventStarted = true;
            puzzlePlayer2.IntroduceNewHole(this);
            puzzlePlayer2.RetrieveBoatSegment(boatSegment);
            puzzlePlayer2.StartSequencePuzzle();
        }
    }
}
