using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSegment : MonoBehaviour
{
    public enum Status
    {
        NoDamage,
        SmallDamage,
        BigDamage
    }

    public Status MyStatus = Status.NoDamage;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite noDamageSprite;
    [SerializeField] private Sprite smallDamageSprite;
    [SerializeField] private Sprite bigDamageSprite;

    [SerializeField] private BoatHealth boatHealth;

    [SerializeField]
    private AudioManager audioManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Change status of the segment
    public void DamageBoatSegment(Status damageType)
    {
        switch (damageType)
        {
            case Status.SmallDamage:
                MyStatus = Status.SmallDamage;
                audioManager.Play("heavydamage");
                break;
            case Status.BigDamage:
                MyStatus = Status.BigDamage;
                audioManager.Play("smalldamage");
                break;
        }
        ChangeToDamageVisuals(damageType);
    }

    // Change the damage visuals
    private void ChangeToDamageVisuals(Status damageType)
    {
        switch (damageType)
        {
            case Status.SmallDamage:
                spriteRenderer.sprite = smallDamageSprite;
                break;
            case Status.BigDamage:
                spriteRenderer.sprite = bigDamageSprite;
                break;
        }
    }

    // Repair boat to undamaged
    public void RepairBoatSegment()
    {
        switch (MyStatus)
        {
            case Status.NoDamage:
                Debug.Log("There is nothing to restore!");
                break;
            case Status.SmallDamage:
                boatHealth.RestoreBoat(DamageManager.SMALLDAMAGE);
                //audioManager.Play("repairsucceeded");
                break;
            case Status.BigDamage:
                boatHealth.RestoreBoat(DamageManager.BIGDAMAGE);
                //audioManager.Play("repairsucceeded");
                break;
        }

        MyStatus = Status.NoDamage;
        spriteRenderer.sprite = noDamageSprite;
    }
}
