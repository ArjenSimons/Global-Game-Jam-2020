using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int countDownTime, startTime;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 3;
        countDownTime = startTime;
        StartCoroutine("Countdown");
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

    public void Pause()
    {

    }

    public void Resume()
    {

    }
}
