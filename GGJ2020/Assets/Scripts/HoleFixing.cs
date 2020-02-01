using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class detects player that collides with a damaged boat segment
/// </summary>
public class HoleFixing : MonoBehaviour
{
    public GameObject player;
    public GameObject aButton;

    private void Update()
    {
    }

    /// <summary>
    /// tries to detect holes in all the boat parts
    /// </summary>
    private void OnCollisionEnter(Collision player)
    {
        //if (this boat segment.stage > 0 )
        //{
        //    show UI "A BUTTON"
        // ShowButtonIndicator();
        //}

    }


    private void ShowButtonIndicator()
    {
        aButton.SetActive(true);
    }
    
    // if button clicked
    //method do GENERATE SEQUENCE PUZZLE when pressed A BUTTON



}
