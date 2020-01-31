using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This script manages the sequence puzzle
/// </summary>
public class SequencePuzzle : MonoBehaviour
{
    public enum Inputs { A, B, X, Y}

    [SerializeField]
    private List<Inputs> inputButtons = new List<Inputs>();

    [Serializable]
    public struct Sequence
    {
        public Image sequenceImage;
        public List<Inputs> correctOrderButtons;
    }

    [SerializeField]
    public List<Sequence> sequences;

    private int buttonsCorrect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            inputButtons.Add(Inputs.A);
            CheckOrder();
            Debug.Log("add A");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            inputButtons.Add(Inputs.B);
            CheckOrder();
            Debug.Log("add B");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            inputButtons.Add(Inputs.X);
            CheckOrder();
            Debug.Log("add X");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            inputButtons.Add(Inputs.Y);
            CheckOrder();
            Debug.Log("add Y");
        }




    }

    private void CheckOrder()
    {
        for (int i = 0; i < inputButtons.Count; i++)
        {
            for (int j = 0; j < sequences.Count; j++)
            {
                if (inputButtons[i] == sequences[0].correctOrderButtons[i])
                {
                    buttonsCorrect++;

                    Debug.Log("buttons correct: " + buttonsCorrect);
                }
                else
                {
                    Debug.Log("you fucked up ");
                }
            }
        }

        if (buttonsCorrect == sequences[0].correctOrderButtons.Count)
        {
            Debug.Log("Solved!");
            buttonsCorrect = 0;
            inputButtons.Clear();
        }
    }
}
