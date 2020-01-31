using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CloudSpeedType
{
    slow,
    normal,
    fast
}

public class CloudMovement : MonoBehaviour
{
    private Random random;

    /// <summary>
    /// The maximum and minimum heights for the randomizer for cloud size.
    /// Offset of max cloud width / 2.
    /// </summary>
    private int maxHeight, minHeight, maxWidth, minWidth, placementOffset;

    /// <summary>
    /// The speed of the cloud, maximum speed of cloud and maximum speed of boat.
    /// Slow and Fast speed offsets for paralax scrolling.
    /// </summary>
    private float speed, maxSpeed, maxBoatSpeed, slowSpeedOffset, fastSpeedOffset;

    /// <summary>
    /// MovementVector used to move the cloud.
    /// </summary>
    Vector3 movementVector;

    public Camera upperCamera;

    public CloudSpeedType cloudSpeedType;


    // Start is called before the first frame update
    void Start()
    {
        random = new Random();
        movementVector = new Vector3();

        minHeight = 2;
        maxHeight = 4;
        minWidth = 2;
        maxWidth = 6;

        //randomizes X and Y scale of clouds
        this.transform.localScale = new Vector3(Random.Range(minWidth, maxWidth), Random.Range(minHeight, maxHeight), this.transform.localScale.z);

        maxSpeed = 100;

        //TODO: set correct speed type

        slowSpeedOffset = -1.5f;
        fastSpeedOffset = 1.5f;

        //TODO: set maxBoatSpeed to maximum speed of boat.

        //TODO: use the changeSpeed method to set the clouds to the right speed.
        speed = maxSpeed / 2;

        Debug.Log(speed);

        if (cloudSpeedType == CloudSpeedType.fast)
        {
            speed += fastSpeedOffset;
        }
        else if (cloudSpeedType == CloudSpeedType.slow)
        {
            speed += slowSpeedOffset;
        }
        Debug.Log(speed);

        movementVector = -Vector3.right * speed / 50;

        Debug.Log(movementVector);

        //TODO: set offset to width of biggest cloud.
        placementOffset = maxWidth / 2;

        


    }

    // Update is called once per frame
    void Update()
    {
        if (upperCamera.WorldToScreenPoint(this.transform.position).x < upperCamera.pixelRect.xMin - placementOffset)
        {
            this.transform.position = new Vector3(upperCamera.ScreenToWorldPoint(new Vector3(upperCamera.pixelWidth, 0, 0)).x + placementOffset, this.transform.position.y, this.transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        this.transform.Translate(movementVector);
    }

    /// <summary>
    /// Changes the speed of the clouds in percentage to speed of boat.
    /// </summary>
    /// <param name="boatSpeed"></param>
    public void ChangeSpeed(float boatSpeed)
    {
        speed = maxSpeed / maxBoatSpeed * boatSpeed;

        if (cloudSpeedType == CloudSpeedType.fast)
        {
            speed += fastSpeedOffset;
        }
        else if (cloudSpeedType == CloudSpeedType.slow)
        {
            speed += slowSpeedOffset;
        }

        movementVector = -Vector3.right * speed;
    }
}
