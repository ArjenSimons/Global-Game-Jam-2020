using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxMovementSpeed = 5, movementDecay = 0.5f, movementSpeedIncrease = 1.2f;
    private float playerSpeed = 0, inputDirectionUpper = 0, inputDirectionUnder = 0;

    private Rigidbody2D rb;

    private bool walking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {
        inputDirectionUpper = Input.GetAxis("UpperJoystickHorizontal");
        inputDirectionUnder = Input.GetAxis("DownJoystickHorizontal");

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

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || inputDirectionUnder == 0 && inputDirectionUpper >= -0.2f && inputDirectionUpper <= 0.2f)
        {
            walking = false;
        }

        if (!walking)
        {
            rb.velocity *= movementDecay;
            playerSpeed = rb.velocity.x;
        }
    }
}
