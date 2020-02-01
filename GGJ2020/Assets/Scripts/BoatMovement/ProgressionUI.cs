using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionUI : MonoBehaviour
{
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private RectTransform playerOneIndicator;
    [SerializeField] private RectTransform playerTwoIndicator;
    [SerializeField] private TextMeshProUGUI speedIndicatorOne;
    [SerializeField] private TextMeshProUGUI speedIndicatorTwo;
    [SerializeField] private BoatMovement boatOne;
    [SerializeField] private BoatMovement boatTwo;

    private float playerOneProgression;
    private float playerTwoProgression;

    private void FixedUpdate()
    {
        playerOneProgression = progressManager.getProgression(Player.PLAYER_ONE);
        playerTwoProgression = progressManager.getProgression(Player.PLAYER_TWO);

        //Set progression indicator position
        playerOneIndicator.transform.localPosition = new Vector2(-910 + playerOneProgression / 100 * 1820, playerOneIndicator.localPosition.y);
        playerTwoIndicator.transform.localPosition = new Vector2(-910 + playerTwoProgression/100 * 1820, playerTwoIndicator.localPosition.y);

        //Set speed indicator text
        speedIndicatorOne.text = Mathf.Round(boatOne.speed) + "km/u";
        speedIndicatorTwo.text = Mathf.Round(boatTwo.speed) + "km/u";
    }
}
