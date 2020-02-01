using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows the type the cloud should be in the paralax scrolling.
/// </summary>
public enum CloudSpeedType
{
    Slow,
    Normal,
    Fast
}

public class CloudMovement : MonoBehaviour
{
    private Random random;

    /// <summary>
    /// The maximum and minimum heights for the randomizer for cloud size.
    /// 100 percent.
    /// </summary>
    private int maxHeight, minHeight, maxWidth, minWidth, oneHundredPerc;

    /// <summary>
    /// The speed of the cloud, maximum speed of cloud and maximum speed of boat.
    /// Slow and Fast speed offsets for paralax scrolling.
    /// </summary>
    private float speed, maxSpeed, maxBoatSpeed, slowSpeedOffset, fastSpeedOffset, offScreenOffset;

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

        oneHundredPerc = 100;

        minHeight = 2;
        maxHeight = 4;
        minWidth = 2;
        maxWidth = 6;

        //randomizes X and Y scale of clouds
        this.transform.localScale = new Vector3(Random.Range(minWidth, maxWidth), Random.Range(minHeight, maxHeight), this.transform.localScale.z);

        maxSpeed = 12;

        if (this.gameObject.name.Contains(CloudSpeedType.Slow.ToString()))
        {
            cloudSpeedType = CloudSpeedType.Slow;
        }
        else if (this.gameObject.name.Contains(CloudSpeedType.Normal.ToString()))
        {
            cloudSpeedType = CloudSpeedType.Normal;
        }
        else if (this.gameObject.name.Contains(CloudSpeedType.Fast.ToString()))
        {
            cloudSpeedType = CloudSpeedType.Fast;
        }

        slowSpeedOffset = -1.5f;
        fastSpeedOffset = 1.5f;

        //TODO: set maxBoatSpeed to maximum speed of boat.

        maxBoatSpeed = GameObject.Find("Boat").GetComponent<BoatMovement>().maxSpeed;

        Debug.Log(maxBoatSpeed);

        //boats start at max speed so clouds should too.
        ChangeSpeed(maxBoatSpeed);

        //

        offScreenOffset = upperCamera.WorldToScreenPoint(new Vector3(this.transform.position.x + this.gameObject.GetComponent<SpriteRenderer>().sprite.rect.width, 0, 0)).x - upperCamera.WorldToScreenPoint(new Vector3(this.transform.position.x, 0, 0)).x;
    }

    // Update is called once per frame
    void Update()
    {
        if (upperCamera.WorldToScreenPoint(this.transform.position).x < upperCamera.pixelRect.xMin - offScreenOffset)
        {
            this.transform.position = new Vector3(upperCamera.ScreenToWorldPoint(new Vector3(upperCamera.pixelWidth + offScreenOffset, 0, 0)).x, this.transform.position.y, this.transform.position.z);
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
        float percentage = oneHundredPerc / maxBoatSpeed * boatSpeed;

        Debug.Log(percentage);

        speed = percentage / oneHundredPerc * maxSpeed;

        Debug.Log(speed);

        if (cloudSpeedType == CloudSpeedType.Fast)
        {
            speed += fastSpeedOffset;
        }
        else if (cloudSpeedType == CloudSpeedType.Slow)
        {
            speed += slowSpeedOffset;
        }

        movementVector = -Vector3.right * speed / 50;

        Debug.Log(movementVector);
    }
}
