﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This script manages the sequence puzzle
/// </summary>
public class SequencePuzzlePlayer1 : MonoBehaviour
{
    public enum ButtonsPlayer1 { A, B, X, Y }

    [SerializeField]
    private ButtonsPlayer1[] correctOrderSmall;

    [SerializeField]
    private ButtonsPlayer1[] correctOrderBig;

    private int currentSequenceButton;
    private int smallOrderMax;
    private int bigOrderMax;

    GameObject boatSegment;
    BoatSegment bS;

    public virtual void Start()
    {
        bS = GetComponentInChildren<BoatSegment>();

        currentSequenceButton = 0;
        smallOrderMax = 4;
        bigOrderMax = 8;

        SequenceRandomizer();
    }


    public virtual void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Check(Buttons.A);
        //}
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
        //    Check(Buttons.B);
        //}
        //else if (Input.GetKeyDown(KeyCode.X))
        //{
        //    Check(Buttons.X);
        //}
        //else if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Check(Buttons.Y);
        //}

        if (Input.GetButtonDown("A-Button1"))
        {
            Check(ButtonsPlayer1.A);
            Debug.Log("press A xbox controller");
        }
        if (Input.GetButtonDown("B-Button1"))
        {
            Check(ButtonsPlayer1.B);
            Debug.Log("press B xbox controller");
        }
        if (Input.GetButtonDown("X-Button1"))
        {
            Check(ButtonsPlayer1.X);
            Debug.Log("press X xbox controller");
        }
        if (Input.GetButtonDown("Y-Button1"))
        {
            Check(ButtonsPlayer1.Y);
            Debug.Log("press Y xbox controller");
        }
    }


    public virtual void SequenceRandomizer()
    {
        // decide whether to use correctOrderSmall or correctOrderBig
        //if (bS.MyStatus == BoatSegment.Status.SmallDamage)
        //{
        for (int i = 0; i < correctOrderSmall.Length; i++)
        {
            correctOrderSmall[i] = (ButtonsPlayer1)UnityEngine.Random.Range(0, 3);
        }
        //}

        //if (bS.MyStatus == BoatSegment.Status.BigDamage)
        //{

        //}
        for (int j = 0; j < correctOrderBig.Length; j++)
        {
            correctOrderBig[j] = (ButtonsPlayer1)UnityEngine.Random.Range(0, 3);
        }
    }

    /// <summary>
    /// Checks the input of player with the current sequence button
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public virtual void Check(ButtonsPlayer1 input)
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
}