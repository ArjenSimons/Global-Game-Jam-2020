using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This script manages the sequence puzzle
/// </summary>
public class SequencePuzzle : MonoBehaviour
{
    public enum Buttons { A, B, X, Y }

    [SerializeField]
    private Buttons[] correctOrderSmall;

    [SerializeField]
    private Buttons[] correctOrderBig;

    int currentSequenceButton;
    int smallOrderMax;
    int bigOrderMax;

    private void Start()
    {
        currentSequenceButton = 0;
        smallOrderMax = 4;
        bigOrderMax = 8;

        SequenceRandomizer();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Check(Buttons.A);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Check(Buttons.B);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Check(Buttons.X);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            Check(Buttons.Y);
        }
        Debug.Log(currentSequenceButton);
    }

    private void SequenceRandomizer()
    {
        for (int i = 0; i < correctOrderSmall.Length; i++)
        {
            correctOrderSmall[i] = (Buttons)UnityEngine.Random.Range(0, 3);
        }
    }

    /// <summary>
    /// Checks the input of player with the current sequence button
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private void Check(Buttons input)
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
