using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : PlayerMovementBase
{
    [SerializeField]
    private float maxMovementSpeed = 5, movementDecay = 0.5f, movementSpeedIncrease = 1.2f, checkRadius = 0;
    private float playerSpeed = 0, inputDirectionUpper = 0, inputDirectionUnder = 0;

    [SerializeField]
    private GameObject boat;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask ground;

    private Transform boatSprite;

    private Rigidbody2D rb;

    private SpriteRenderer boatRender;

    private bool walking, grounded;

    public bool canWalk;

    // Start is called before the first frame update
    void Start()
    {
        canWalk = true;
        rb = GetComponent<Rigidbody2D>();
        boatSprite = boat.transform.GetChild(boat.transform.childCount - 1);
        boatRender = boatSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canWalk)
        {
            movement();
        }
    }

    private void FixedUpdate()
    {
        clampPosition();
        playerOnGround();
    }

    private void clampPosition()
    {
        var boatPos = boat.transform.position;
        var pos = transform.position;
        if (transform.position.x > boatPos.x + boatRender.size.x * boatSprite.transform.localScale.x / 2)
        {
            pos.x = boatPos.x + boatRender.size.x * boatSprite.transform.localScale.x / 2;
            transform.position = pos;
        }
        else if (transform.position.x < boatPos.x - boatRender.size.x * boatSprite.transform.localScale.x / 2)
        {
            pos.x = boatPos.x - boatRender.size.x * boatSprite.transform.localScale.x / 2;
            transform.position = pos;
        }
    }

    private void playerOnGround()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);

        if (grounded)
        {
            rb.gravityScale = 0;
        } else
        {
            rb.gravityScale = 15;
        }
    }

    private void movement()
    {
        inputDirectionUpper = Input.GetAxis("UpperJoystickHorizontal1");
        inputDirectionUnder = Input.GetAxis("DownJoystickHorizontal1");

        if (Input.GetKey(KeyCode.A) || inputDirectionUnder < 0 || inputDirectionUpper < -0.2f)
        {
            walking = true;

            transform.localScale = new Vector3(0 - Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

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

            transform.localScale = new Vector3(0 + Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

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

        animController.SetBool("IsWalking", walking);

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || inputDirectionUnder == 0 && inputDirectionUpper >= -0.2f && inputDirectionUpper <= 0.2f)
        {
            walking = false;
        }
    }  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "CanonballStack" && !CarryingCanonBall)
        {
            CarryingCanonBall = true;
        }
        
    }
}
