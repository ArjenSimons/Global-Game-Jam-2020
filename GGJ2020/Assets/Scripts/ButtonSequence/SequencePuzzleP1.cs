using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This script manages the sequence puzzle PLAYER 1
/// </summary>
public class SequencePuzzleP1 : MonoBehaviour
{
    public enum ButtonsP1 { A, B, X, Y }

    [SerializeField]
    private Player player;

    [SerializeField]
    private ButtonsP1[] correctOrderSmall;

    [SerializeField]
    private ButtonsP1[] correctOrderBig;

    public Sprite[] buttonSprites;
    public GameObject[] buttons;

    private int currentSequenceButton;
    private int smallOrderMax;
    private int bigOrderMax;

    public bool isActivated;

    BoatSegment bS;

    public Canvas playerCanvas;

    private void Start()
    {
        currentSequenceButton = 0;
        smallOrderMax = correctOrderSmall.Length;
        bigOrderMax = correctOrderBig.Length;
    }


    private void Update()
    {
        if (isActivated)
        {
            if (player == Player.PLAYER_ONE)
            {
                if (Input.GetKeyDown("joystick 2 button 1"))
                {
                    Check(ButtonsP1.A);
                    Debug.Log("press A logitech controller");
                }

                else if (Input.GetKeyDown("joystick 2 button 2"))
                {
                    Check(ButtonsP1.B);
                    Debug.Log("press B logitech controller");
                }

                else if (Input.GetKeyDown("joystick 2 button 0"))
                {
                    Check(ButtonsP1.X);
                    Debug.Log("press X logitech controller");
                }
                else if (Input.GetKeyDown("joystick 2 button 3"))
                {
                    Check(ButtonsP1.Y);
                    Debug.Log("press Y logitech controller");
                }
            }
        }
        // PLAYER ONE usese xbox controller
        

        //InputKeyboard();
    }

    public void SequenceRandomizer()
    {
        // decide whether to use correctOrderSmall or correctOrderBig

        if (bS.MyStatus == BoatSegment.Status.SmallDamage)
        {
            for (int i = 0; i < correctOrderSmall.Length; i++)
            {

                int randomNumber = UnityEngine.Random.Range(0, 3);
                correctOrderSmall[i] = (ButtonsP1)randomNumber;
                buttons[i].GetComponent<Image>().sprite = buttonSprites[randomNumber];
            }
        }

        if (bS.MyStatus == BoatSegment.Status.BigDamage)
        {
                for (int j = 0; j < correctOrderBig.Length; j++)
                {
                    int randomNumber = UnityEngine.Random.Range(0, 3);
                    correctOrderBig[j] = (ButtonsP1)randomNumber;
                    buttons[j].GetComponent<Image>().sprite = buttonSprites[randomNumber];
                }
        }
    }

    public void StartSequencePuzzle()
    {
        playerCanvas.gameObject.SetActive(true);
        ResetSequencePuzzle();
        isActivated = true;
        Debug.Log("Started puzzle");
    }

    public void StopSequencePuzzle()
    {
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
        StopSequencePuzzle();
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
        StopSequencePuzzle();

        //ToDo: call method that deletes hole.

        Debug.Log("Sequence was solved.");
    }

    public void SequenceFailed()
    {
        ResetSequencePuzzle();

        Debug.Log("Sequence was failed.");
    }

    /// <summary>
    /// Checks the input of player with the current sequence button
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private void Check(ButtonsP1 input)
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
        Debug.Log("Bs is nu de boatsegment..");
    }

    /// <summary>
    /// Method that reads user input thru keyboard
    /// </summary>
    private void InputKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Check(ButtonsP1.A);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Check(ButtonsP1.B);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Check(ButtonsP1.X);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            Check(ButtonsP1.Y);
        }
    }
}
