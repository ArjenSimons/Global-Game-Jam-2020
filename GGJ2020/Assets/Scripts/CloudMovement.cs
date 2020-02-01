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
    private float speed, minSpeed, maxSpeed, maxBoatSpeed, slowSpeedOffset, fastSpeedOffset, offScreenOffset;

    /// <summary>
    /// MovementVector used to move the cloud.
    /// </summary>
    Vector3 movementVector;

    private GameObject parentBoat;

    public Camera upperCamera;

    public CloudSpeedType cloudSpeedType;


    // Start is called before the first frame update
    void Start()
    {
        random = new Random();
        movementVector = new Vector3();

        oneHundredPerc = 100;

        maxSpeed = 3;
        minSpeed = 0.2f;

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

        slowSpeedOffset = -0.5f;
        fastSpeedOffset = 0.5f;

        if (this.gameObject.name.Contains("1"))
        {
            parentBoat = GameObject.Find("Boat1");
            this.gameObject.tag = tag + "1";
        }
        else if (this.gameObject.name.Contains("2"))
        {
            parentBoat = GameObject.Find("Boat2");
            this.gameObject.tag = tag + "2";
        }
        maxBoatSpeed = parentBoat.GetComponent<BoatMovement>().maxSpeed;

        //boats start at max speed so clouds should too.
        ChangeSpeed(maxBoatSpeed);
       
        offScreenOffset = this.gameObject.GetComponent<SpriteRenderer>().sprite.rect.width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = upperCamera.WorldToScreenPoint(this.transform.position);
        if (screenPosition.x < -offScreenOffset)
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

        speed = percentage / oneHundredPerc * maxSpeed;

        if (cloudSpeedType == CloudSpeedType.Fast)
        {
            speed += fastSpeedOffset;
            speed = (maxSpeed - minSpeed) * speed / maxSpeed + minSpeed;
        }
        else if (cloudSpeedType == CloudSpeedType.Slow)
        {
            speed += slowSpeedOffset;

            speed = (maxSpeed - minSpeed) * speed / maxSpeed + minSpeed;
        }

        movementVector = -Vector3.right * speed / 50;
    }
}
