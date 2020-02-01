﻿using System;
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

    public Sprite[] buttonSprites;
    public GameObject[] buttons;

    private int currentSequenceButton;
    private int smallOrderMax;
    private int bigOrderMax;

    private GameObject boatSegment;

    BoatSegment bS;

    private void Start()
    {
        bS = GetComponent<BoatSegment>();

        currentSequenceButton = 0;
        smallOrderMax = 4;
        bigOrderMax = 8;


    }

    private void Update()
    {
        // PLAYER TWO uses xbox controller
        if (player == Player.PLAYER_TWO)
        {
            if (Input.GetButtonDown("A-Button1"))
            {
                Check(ButtonsP2.A);
                Debug.Log("press A xbox controller");
            }
            else if (Input.GetButtonDown("B-Button1"))
            {
                Check(ButtonsP2.B);
                Debug.Log("press B xbox controller");
            }
            else if (Input.GetButtonDown("X-Button1"))
            {
                Check(ButtonsP2.X);
                Debug.Log("press X xbox controller");
            }
            else if (Input.GetButtonDown("Y-Button1"))
            {
                Check(ButtonsP2.Y);
                Debug.Log("press Y xbox controller");
            }
        }



        //InputKeyboard();
    }

    /// <summary>
    /// Randomize the button combination in the sequence puzzle
    /// </summary>
    public void SequenceRandomizer()
    {
        // decide whether to use correctOrderSmall or correctOrderBig

        if (bS.MyStatus == BoatSegment.Status.SmallDamage)
        {
            Debug.Log("RANDOMIZE2");
            for (int i = 0; i < correctOrderSmall.Length; i++)
            {

                int randomNumber = UnityEngine.Random.Range(0, 3);
                correctOrderSmall[i] = (ButtonsP2)randomNumber;
                buttons[i].GetComponent<Image>().sprite = buttonSprites[randomNumber];
            }
        }

        if (bS.MyStatus == BoatSegment.Status.BigDamage)
        {
            Debug.Log("RANDOMIZE");
            for (int j = 0; j < correctOrderBig.Length; j++)
                correctOrderBig[j] = (ButtonsP2)UnityEngine.Random.Range(0, 3);
            for (int j = 0; j < correctOrderBig.Length; j++)
            {
                int randomNumber = UnityEngine.Random.Range(0, 3);
                correctOrderBig[j] = (ButtonsP2)randomNumber;
                buttons[j].GetComponent<Image>().sprite = buttonSprites[randomNumber];

                Debug.Log("grote");
            }

        }

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
                currentSequenceButton++;
                Debug.Log("solved a thing");
                if (currentSequenceButton == smallOrderMax)
                {
                    Debug.Log(" you just solved the whole damn puzzle!");
                    currentSequenceButton = 0;
                    //delete hole
                }
            }
            else
            {
                Debug.Log("you fucked up xD");
                currentSequenceButton = 0;
                // generate a new sequence
                SequenceRandomizer();
            }
        }
        else if (bS.MyStatus == BoatSegment.Status.BigDamage)
        {
            if (input == correctOrderBig[currentSequenceButton])
            {
                currentSequenceButton++;
                Debug.Log("solved a thing");
                if (currentSequenceButton == bigOrderMax)
                {
                    Debug.Log(" you just solved the whole damn puzzle!");
                    currentSequenceButton = 0;
                    //delete hole
                }
            }
            else
            {
                Debug.Log("you fucked up xD");
                currentSequenceButton = 0;
                // generate a new sequence
                SequenceRandomizer();
            }
        }
    }

    public void RetrieveBoatSegment(BoatSegment brokenBoatSegment)
    {
        bS = brokenBoatSegment;
    }

    /// <summary>
    /// Method that reads user input thru keyboard
    /// </summary>
    private void InputKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Check(ButtonsP2.A);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Check(ButtonsP2.B);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Check(ButtonsP2.X);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            Check(ButtonsP2.Y);
        }
    }
}