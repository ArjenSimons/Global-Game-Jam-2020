using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class detects player that collides with a damaged boat segment
/// </summary>
public class HoleFixing : MonoBehaviour
{
    public GameObject player;
    public GameObject repairButtonIndicator;
    private bool mayPressBtnA;

    private const string PLAYER_TAG = "Player";

    private BoatSegment boatSegment;

    private void Start()
    {
        boatSegment = GetComponent<BoatSegment>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && mayPressBtnA)
        {
            DisplaySequencePuzzle();
        }
    }

    /// <summary>
    /// tries to detect holes in all the boat parts
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG &&
            (boatSegment.MyStatus == BoatSegment.Status.SmallDamage ||
            boatSegment.MyStatus == BoatSegment.Status.BigDamage))
        {
            ShowButtonIndicator();
            mayPressBtnA = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG &&
            (boatSegment.MyStatus == BoatSegment.Status.SmallDamage ||
            boatSegment.MyStatus == BoatSegment.Status.BigDamage))
        {
            HideButtonIndicator();
            mayPressBtnA = false;
        }
    }

    // Shows the activate repair button
    private void ShowButtonIndicator()
    {
        repairButtonIndicator.SetActive(true);
    }

    // Hides the activate repair button
    private void HideButtonIndicator()
    {
        repairButtonIndicator.SetActive(false);
    }

    // method do GENERATE SEQUENCE PUZZLE when pressed A BUTTON
    private void DisplaySequencePuzzle()
    {
        HideButtonIndicator();
        mayPressBtnA = false;

        Debug.Log("Je ziet nu de puzzel");
    }
}
