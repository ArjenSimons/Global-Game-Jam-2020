using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField]
    private float amplitude, screenShakeTime;

    private float screenShakeTimer = 0;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (screenShakeTimer > 0)
        {
            screenShakeTimer -= Time.deltaTime;
            transform.localPosition = startPos + Random.insideUnitSphere * amplitude;

            if (screenShakeTimer <= 0)
            {
                transform.position = startPos;
            }
        }
    }

    public void activateScreenShake()
    {
        screenShakeTimer = screenShakeTime;
    }
}
