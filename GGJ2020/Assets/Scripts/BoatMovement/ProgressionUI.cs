using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionUI : MonoBehaviour
{
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private RectTransform playerOneIndicator;
    [SerializeField] private RectTransform playerTwoIndicator;

    private float playerOneProgression;
    private float playerTwoProgression;

    private void FixedUpdate()
    {
        playerOneProgression = progressManager.getProgression(Player.PLAYER_ONE);
        playerTwoProgression = progressManager.getProgression(Player.PLAYER_TWO);

        //TODO: Update position of playterIndicators on canvas

        playerOneIndicator.transform.localPosition = new Vector2(-910 + playerOneProgression / 100 * 1820, playerOneIndicator.localPosition.y);
        playerTwoIndicator.transform.localPosition = new Vector2(-910 + playerTwoProgression/100 * 1820, playerTwoIndicator.localPosition.y);
    }
}
