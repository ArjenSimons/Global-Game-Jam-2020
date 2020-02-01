using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This script manages the sequence puzzle
/// </summary>
public class SequencePuzzlePlayer2 : MonoBehaviour
{
    public enum ButtonsPlayer2 { A, B, X, Y }

    [SerializeField]
    private Player player;

    [SerializeField]
    private ButtonsPlayer2[] correctOrderSmall;

    [SerializeField]
    private ButtonsPlayer2[] correctOrderBig;

    private int currentSequenceButton;
    private int smallOrderMax;
    private int bigOrderMax;

    GameObject boatSegment;
    BoatSegment bS;

    private void Start()
    {
        bS = GetComponentInChildren<BoatSegment>();

        currentSequenceButton = 0;
        smallOrderMax = 4;
        bigOrderMax = 8;

        SequenceRandomizer();
    }


    private void Update()
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

        //if (player == Player.PLAYER_ONE)
        //{
        //    if (Input.GetButtonDown("A-Button1"))
        //    {
        //        Check(ButtonsPlayer2.A);
        //        Debug.Log("press A xbox controller");
        //    }
        //    if (Input.GetButtonDown("B-Button1"))
        //    {
        //        Check(ButtonsPlayer2.B);
        //        Debug.Log("press B xbox controller");
        //    }
        //    if (Input.GetButtonDown("X-Button1"))
        //    {
        //        Check(ButtonsPlayer2.X);
        //        Debug.Log("press X xbox controller");
        //    }
        //    if (Input.GetButtonDown("Y-Button1"))
        //    {
        //        Check(ButtonsPlayer2.Y);
        //        Debug.Log("press Y xbox controller");
        //    }

        //}


        if (player == Player.PLAYER_ONE)
        {
            if (Input.GetKeyDown("joystick 2 button 1"))
            {
                Check(ButtonsPlayer2.A);
                Debug.Log("press A logitech controller");
            }

            else if (Input.GetKeyDown("joystick 2 button 2"))
            {
                Check(ButtonsPlayer2.B);
                Debug.Log("press B logitech controller");
            }

            else if (Input.GetKeyDown("joystick 2 button 0"))
            {
                Check(ButtonsPlayer2.X);
                Debug.Log("press X logitech controller");
            }
            else if (Input.GetKeyDown("joystick 2 button 3"))
            {
                Check(ButtonsPlayer2.Y);
                Debug.Log("press Y logitech controller");
            }
        }
    }


    private void SequenceRandomizer()
    {
        // decide whether to use correctOrderSmall or correctOrderBig
        //if (bS.MyStatus == BoatSegment.Status.SmallDamage)
        //{
        for (int i = 0; i < correctOrderSmall.Length; i++)
        {
            correctOrderSmall[i] = (ButtonsPlayer2)UnityEngine.Random.Range(0, 3);
        }
        //}

        //if (bS.MyStatus == BoatSegment.Status.BigDamage)
        //{

        //}
        for (int j = 0; j < correctOrderBig.Length; j++)
        {
            correctOrderBig[j] = (ButtonsPlayer2)UnityEngine.Random.Range(0, 3);
        }
    }

    /// <summary>
    /// Checks the input of player with the current sequence button
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private void Check(ButtonsPlayer2 input)
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
