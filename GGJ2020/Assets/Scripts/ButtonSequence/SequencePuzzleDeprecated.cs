//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
/////  This script manages the sequence puzzle
///// </summary>
//public class SequencePuzzleDeprecated : MonoBehaviour
//{
//    public enum Buttons { A, B, X, Y }

//    [SerializeField]
//    private Player player;

//    [SerializeField]
//    private Buttons[] correctOrderSmall;

//    [SerializeField]
//    private Buttons[] correctOrderBig;

//    private int currentSequenceButton;
//    private int smallOrderMax;
//    private int bigOrderMax;

//    GameObject boatSegment;
//    BoatSegment bS;

//    public virtual void Start()
//    {
//        bS = GetComponentInChildren<BoatSegment>();

//        currentSequenceButton = 0;
//        smallOrderMax = 4;
//        bigOrderMax = 8;

//        SequenceRandomizer();
//    }


//    public virtual void Update()
//    {
//        //if (Input.GetKeyDown(KeyCode.A))
//        //{
//        //    Check(Buttons.A);
//        //}
//        //else if (Input.GetKeyDown(KeyCode.B))
//        //{
//        //    Check(Buttons.B);
//        //}
//        //else if (Input.GetKeyDown(KeyCode.X))
//        //{
//        //    Check(Buttons.X);
//        //}
//        //else if (Input.GetKeyDown(KeyCode.Y))
//        //{
//        //    Check(Buttons.Y);
//        //}

//        if (player == Player.PLAYER_ONE)
//        {
//            if (Input.GetButtonDown("A-Button1"))
//            {
//                Check(Buttons.A);
//                Debug.Log("press A xbox controller");
//            }
//            if (Input.GetButtonDown("B-Button1"))
//            {
//                Check(Buttons.B);
//                Debug.Log("press B xbox controller");
//            }
//            if (Input.GetButtonDown("X-Button1"))
//            {
//                Check(Buttons.X);
//                Debug.Log("press X xbox controller");
//            }
//            if (Input.GetButtonDown("Y-Button1"))
//            {
//                Check(Buttons.Y);
//                Debug.Log("press Y xbox controller");
//            }
//        }

//        if (player == Player.PLAYER_TWO)
//        {
//            if (Input.GetButtonDown("A-Button2"))
//            {
//                Check(Buttons.A);
//                Debug.Log("press A logitech controller");
//            }

//            if (Input.GetButtonDown("B-Button2"))
//            {
//                Check(Buttons.B);
//                Debug.Log("press B logitech controller");
//            }

//            if (Input.GetButtonDown("X-Button2"))
//            {
//                Check(Buttons.X);
//                Debug.Log("press X logitech controller");
//            }
//            if (Input.GetButtonDown("Y-Button2"))
//            {
//                Check(Buttons.Y);
//                Debug.Log("press Y logitech controller");
//            }
//        }
       


//    }


//    public virtual void SequenceRandomizer()
//    {
//        // decide whether to use correctOrderSmall or correctOrderBig
//        //if (bS.MyStatus == BoatSegment.Status.SmallDamage)
//        //{
//        for (int i = 0; i < correctOrderSmall.Length; i++)
//        {
//            correctOrderSmall[i] = (Buttons)UnityEngine.Random.Range(0, 3);
//        }
//        //}

//        //if (bS.MyStatus == BoatSegment.Status.BigDamage)
//        //{

//        //}
//        for (int j = 0; j < correctOrderBig.Length; j++)
//        {
//            correctOrderBig[j] = (Buttons)UnityEngine.Random.Range(0, 7);
//        }
//    }

//    /// <summary>
//    /// Checks the input of player with the current sequence button
//    /// </summary>
//    /// <param name="input"></param>
//    /// <returns></returns>
//    public virtual void Check(Buttons input)
//    {
//        if (input == correctOrderSmall[currentSequenceButton])
//        {
//            currentSequenceButton++;
//            Debug.Log("solved a thing");
//            if (currentSequenceButton == smallOrderMax)
//            {
//                Debug.Log(" you just solved the whole damn puzzle!");
//                currentSequenceButton = 0;
//                //delete hole
//            }
//        }
//        else
//        {
//            Debug.Log("you fucked up xD");
//            currentSequenceButton = 0;
//            // generate a new sequence
//            SequenceRandomizer();
//        }
//    }
//}
