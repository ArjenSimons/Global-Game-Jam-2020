using UnityEngine;
using HitStatus = Canon.HitStatus;

public class CanonBall : MonoBehaviour
{
    public bool Hit { get; private set; } = false;
    private GameObject targetBoat;
    private bool fallingOnOpponent = false;
    public HitStatus status { get; private set; }

    public void SetFallingOnOpponent(Player player)
    {
        fallingOnOpponent = true;
        string targetName = player == Player.PLAYER_ONE ? "Boat1" : "Boat2";       
         GameObject[] boats = GameObject.FindGameObjectsWithTag("Boat");
         for(int i = 0; i < boats.Length; i++)
         {
            if(boats[i].name == targetName)
            {
                targetBoat = boats[i];
                break;
            }
         }
    }

    private void FixedUpdate()
    {
        if (fallingOnOpponent)
        {
            GameObject boatSpriteObj = targetBoat.transform.GetChild(targetBoat.transform.childCount - 1).gameObject;
            SpriteRenderer spriteRend = boatSpriteObj.GetComponent<SpriteRenderer>();
            if (this.GetComponent<CircleCollider2D>().bounds.Intersects(spriteRend.bounds))
            {
                Hit = true;
                status = HitStatus.STATUS_HIT;
            }
            else if (transform.position.y - (GetComponent<SpriteRenderer>().sprite.rect.height * 0.5f) <
                boatSpriteObj.transform.position.y - (spriteRend.sprite.rect.height * 0.5f))
            {
                Hit = true;
                status = HitStatus.STATUS_MISSED;
            }
        }
    }

    public void ResetBall()
    {
        Hit = false;
        targetBoat = null;
        fallingOnOpponent = false;
    }
}
