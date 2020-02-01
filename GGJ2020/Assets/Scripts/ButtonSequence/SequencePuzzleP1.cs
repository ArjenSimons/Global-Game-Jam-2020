using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // PLAYER ONE usese logitech controller
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

        //InputKeyboard();
    }


    private void SequenceRandomizer()
    {
        // decide whether to use correctOrderSmall or correctOrderBig

        if (bS.MyStatus == BoatSegment.Status.SmallDamage)
        {
            for (int i = 0; i < correctOrderSmall.Length; i++)
                correctOrderSmall[i] = (ButtonsP1)UnityEngine.Random.Range(0, 3);
        }

        if (bS.MyStatus == BoatSegment.Status.BigDamage)
        {

            for (int j = 0; j < correctOrderBig.Length; j++)
                correctOrderBig[j] = (ButtonsP1)UnityEngine.Random.Range(0, 3);
        }
    }

    /// <summary>
    /// Checks the input of player with the current sequence button
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private void Check(ButtonsP1 input)
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
