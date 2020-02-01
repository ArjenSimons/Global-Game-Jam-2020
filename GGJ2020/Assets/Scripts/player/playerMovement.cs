using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxMovementSpeed = 5, movementDecay = 0.5f, movementSpeedIncrease = 1.2f;
    private float playerSpeed = 0, inputDirectionUpper = 0, inputDirectionUnder = 0, boatSize, amountOfChilds;

    [SerializeField]
    private GameObject boat;

    private Rigidbody2D rb;

    private SpriteRenderer boatRender, playerRender;

    private bool walking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boatRender = boat.transform.GetChild(boat.transform.childCount - 1).GetComponent<SpriteRenderer>();
        playerRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void FixedUpdate()
    {
        clampPosition();
    }

    private void clampPosition()
    {
        var boatPos = boat.transform.position;
        var pos = transform.position;
        if (transform.position.x > boatPos.x + ((boatRender.size.x / 2) - playerRender.size.x + 0.1f))
        {
            pos.x = boatPos.x + ((boatRender.size.x / 2) - playerRender.size.x + 0.1f);
            transform.position = pos;
        }
        else if (transform.position.x < boatPos.x - boatRender.size.x / 2)
        {
            pos.x = boatPos.x - boatRender.size.x / 2;
            transform.position = pos;
        }
    }

    private void movement()
    {
        inputDirectionUpper = Input.GetAxis("UpperJoystickHorizontal1");
        inputDirectionUnder = Input.GetAxis("DownJoystickHorizontal1");

        if (Input.GetKey(KeyCode.A) || inputDirectionUnder < 0 || inputDirectionUpper < -0.2f)
        {
            walking = true;

            playerSpeed -= movementSpeedIncrease;
            if (playerSpeed <= -maxMovementSpeed)
            {
                playerSpeed = -maxMovementSpeed;
            }

            rb.velocity = transform.right * playerSpeed;
        }

        if (Input.GetKey(KeyCode.D) || inputDirectionUnder > 0 || inputDirectionUpper > 0.2f)
        {
            walking = true;

            playerSpeed += movementSpeedIncrease;
            if (playerSpeed >= maxMovementSpeed)
            {
                playerSpeed = maxMovementSpeed;
            }

            rb.velocity = transform.right * playerSpeed;
        }

        if (!walking)
        {
            rb.velocity *= movementDecay;
            playerSpeed = rb.velocity.x;
        }


        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || inputDirectionUnder == 0 && inputDirectionUpper >= -0.2f && inputDirectionUpper <= 0.2f)
        {
            walking = false;
        }
    }
}
