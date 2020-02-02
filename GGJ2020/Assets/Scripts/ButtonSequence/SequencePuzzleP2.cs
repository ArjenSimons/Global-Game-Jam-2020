using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This script manages the sequence puzzle PLAYER2
/// </summary>
public class SequencePuzzleP2 : MonoBehaviour
{
    public enum ButtonsP2 { A, B, X, Y }

    [SerializeField]
    private Player player;

    [SerializeField]
    private ButtonsP2[] correctOrderSmall;

    [SerializeField]
    private ButtonsP2[] correctOrderBig;

    private playerMovement movementScript2;

    public Sprite[] buttonSprites;
    public GameObject[] buttons;

    private int currentSequenceButton;
    private int smallOrderMax;
    private int bigOrderMax;

    public bool isActivated;

    BoatSegment bS;

    public Canvas playerCanvas;

    private HoleFixing currentHole;

    private void Start()
    {
        currentSequenceButton = 0;
        smallOrderMax = correctOrderSmall.Length;
        bigOrderMax = correctOrderBig.Length;
        movementScript2 = this.gameObject.GetComponent<playerMovement>();
    }

    private void Update()
    {
        if (isActivated)
        {
            // PLAYER TWO uses xbox controller
            if (player == Player.PLAYER_TWO)
            {
                if (Input.GetButtonDown("A-Button1"))
                {
                    Check(ButtonsP2.A);
                    Debug.Log("press A xbox controller");
                }
                if (Input.GetButtonDown("B-Button1"))
                {
                    Check(ButtonsP2.B);
                    Debug.Log("press B xbox controller");
                }
                if (Input.GetButtonDown("X-Button1"))
                {
                    Check(ButtonsP2.X);
                    Debug.Log("press X xbox controller");
                }
                if (Input.GetButtonDown("Y-Button1"))
                {
                    Check(ButtonsP2.Y);
                    Debug.Log("press Y xbox controller");
                }
            }
        }
    }

    /// <summary>
    /// Randomize the button combination in the sequence puzzle
    /// </summary>
    public void SequenceRandomizer()
    {
        // decide whether to use correctOrderSmall or correctOrderBig

        if (bS.MyStatus == BoatSegment.Status.SmallDamage)
        {
            for (int i = 0; i < correctOrderSmall.Length; i++)
            {
                int randomNumber = UnityEngine.Random.Range(0, 3);
                correctOrderSmall[i] = (ButtonsP2)randomNumber;
                buttons[i].GetComponent<Image>().sprite = buttonSprites[randomNumber];
            }
        }

        if (bS.MyStatus == BoatSegment.Status.BigDamage)
        {
            for (int j = 0; j < correctOrderBig.Length; j++)
            {
                int randomNumber = UnityEngine.Random.Range(0, 3);
                correctOrderBig[j] = (ButtonsP2)randomNumber;
                buttons[j].GetComponent<Image>().sprite = buttonSprites[randomNumber];
            }

        }
    }

    public void StartSequencePuzzle()
    {
        movementScript2.canMove = false;
        playerCanvas.gameObject.SetActive(true);
        ResetSequencePuzzle();
        isActivated = true;
        Debug.Log("Started puzzle");
    }

    public void StopSequencePuzzle()
    {
        movementScript2.canMove = true;
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Image>().color = new Color(255, 255, 255);
        }
        playerCanvas.gameObject.SetActive(false);
        Debug.Log("Ended puzzle");
    }

    public void ResetSequencePuzzle()
    {
        currentSequenceButton = 0;
        SequenceRandomizer();
        Debug.Log("Puzzle Reset");
    }

    public void RightButtonWasPressed()
    {
        buttons[currentSequenceButton].GetComponent<Image>().color = new Color(0, 0, 0);
        currentSequenceButton++;
        Debug.Log("The right Button was pressed.");
    }

    public void SequenceSolved()
    {
        isActivated = false;
        ResetSequencePuzzle();
        StopSequencePuzzle();

        bS.RepairBoatSegment();

        Debug.Log("Sequence was solved.");
    }

    public void SequenceFailed()
    {
        ResetSequencePuzzle();
        StopSequencePuzzle();

        foreach (GameObject button in buttons)
        {
            button.GetComponent<Image>().color = new Color(255, 255, 255);
        }

        if (currentHole != null)
        {
            RestartRepairIndicator();
        }

        //bS.GetComponent<HoleFixing>().ResetBtnA();
        Debug.Log("Sequence was failed.");
    }

    /// <summary>
    /// Checks the input of player with the current sequence button
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private void Check(ButtonsP2 input)
    {
        if (bS.MyStatus == BoatSegment.Status.SmallDamage)
        {
            if (input == correctOrderSmall[currentSequenceButton])
            {
                RightButtonWasPressed();

                if (currentSequenceButton == smallOrderMax)
                {
                    SequenceSolved();
                }
            }
            else
            {
                SequenceFailed();
            }
        }
        else if (bS.MyStatus == BoatSegment.Status.BigDamage)
        {
            if (input == correctOrderBig[currentSequenceButton])
            {
                RightButtonWasPressed();

                if (currentSequenceButton == bigOrderMax)
                {
                    SequenceSolved();
                }
            }
            else
            {
                SequenceFailed();
            }
        }
    }

    public void RetrieveBoatSegment(BoatSegment brokenBoatSegment)
    {
        bS = brokenBoatSegment;
    }

    // New reference to hole
    public void IntroduceNewHole(HoleFixing hole)
    {
        currentHole = hole;
    }

    // Restarts the repair indicator
    private void RestartRepairIndicator()
    {
        currentHole.ShowButtonIndicator();
        currentHole.ResetBtnA();
    }
}
