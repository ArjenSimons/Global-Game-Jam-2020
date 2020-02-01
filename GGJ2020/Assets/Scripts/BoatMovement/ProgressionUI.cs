using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionUI : MonoBehaviour
{
    [SerializeField] private Image playerOneIndicator;
    [SerializeField] private Image playerTwoIndicator;

    private float playerOneProgression;
    private float playerTwoProgression;

    private void FixedUpdate()
    {
        playerOneProgression = ProgressManager.getProgression(Player.PLAYER_ONE);
        playerTwoProgression = ProgressManager.getProgression(Player.PLAYER_TWO);

        //TODO: Update position of playterIndicators on canvas
    }
}
