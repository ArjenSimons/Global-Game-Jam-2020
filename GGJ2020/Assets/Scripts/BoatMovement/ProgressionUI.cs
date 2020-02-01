using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionUI : MonoBehaviour
{
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private Image playerOneIndicator;
    [SerializeField] private Image playerTwoIndicator;

    private float playerOneProgression;
    private float playerTwoProgression;

    private void FixedUpdate()
    {
        playerOneProgression = progressManager.getProgression(Player.PLAYER_ONE);
        playerTwoProgression = progressManager.getProgression(Player.PLAYER_TWO);

        //TODO: Update position of playterIndicators on canvas
    }
}
