using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private int countDownTime, startTime;
    public Text countDown, P1GetReadyText, P2GetReadyText, P1ReadyText, P2ReadyText;

    private bool paused, P1Ready, P2Ready, countDownStarted;

    private BoatMovement boat1, boat2;
    private playerMovement player2movement;
    private player2Movement playermovement;

    [SerializeField]
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 3;
        countDownTime = startTime;
        paused = true;
        P1Ready = false;
        P2Ready = false;
        countDownStarted = false;

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
        if (Input.GetKeyDown("joystick 2 button 1") || Input.GetKeyDown(KeyCode.T))
        {
            Pause();
            text.text = countDownTime.ToString();
            //audioManager.Play("countdown");
            Debug.Log("Player one ready");
            P1IsReady();
        }

        if (Input.GetButtonDown("A-Button1") || Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Player two ready");
            P2IsReady();
        }

        if (P1Ready && P2Ready)
        {
            if (!countDownStarted)
            {
                StartCountDown();
            }


            if (countDownTime > 0)
            {
                Pause();
                countDown.text = countDownTime.ToString();
            }
            else
            {
                countDown.text = "GO!";
                Resume();
                StartCoroutine("FadeGo");
            }
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
            countDown.gameObject.SetActive(false);
            P1ReadyText.gameObject.SetActive(false);
            P2ReadyText.gameObject.SetActive(false);
        }
    }

    public void P1IsReady()
    {
        P1Ready = true;
        P1GetReadyText.gameObject.SetActive(false);
        P1ReadyText.gameObject.SetActive(true);
    }

    public void P2IsReady()
    {
        P2Ready = true;
        P2GetReadyText.gameObject.SetActive(false);
        P2ReadyText.gameObject.SetActive(true);
    }

    public void StartCountDown()
    {
        StartCoroutine("Countdown");
        countDownStarted = true;
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
