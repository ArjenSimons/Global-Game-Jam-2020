using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private int countDownTime, startTime;
    public Text text;

    private bool paused;

    private BoatMovement boat1, boat2;
    private playerMovement player2movement;
    private player2Movement playermovement;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 3;
        countDownTime = startTime;
        StartCoroutine("Countdown");
        paused = true;

        boat1 = GameObject.Find("Boat1").GetComponent<BoatMovement>();
        boat1.paused = paused;

        boat2 = GameObject.Find("Boat2").GetComponent<BoatMovement>();
        boat2.paused = paused;

        playermovement = GameObject.Find("Player1").GetComponent<player2Movement>();
        playermovement.paused = paused;

        player2movement = GameObject.Find("Player2").GetComponent<playerMovement>();
        player2movement.paused = paused;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDownTime > 0)
        {
            Pause();
            text.text = countDownTime.ToString();
        }
        else
        {
            text.text = "GO!";
            Resume();
            StartCoroutine("FadeGo");
        }
    }

    public IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            countDownTime--;
        }
    }

    public IEnumerator FadeGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Destroy(text);
        }
    }

    public void Pause()
    {
        paused = true;
        boat1.paused = paused;
        boat2.paused = paused;
        playermovement.paused = paused;
        player2movement.paused = paused;
    }

    public void Resume()
    {
        paused = false;
        boat1.paused = paused;
        boat2.paused = paused;
        playermovement.paused = paused;
        player2movement.paused = paused;
    }
}
