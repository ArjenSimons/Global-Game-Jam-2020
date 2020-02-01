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

    [SerializeField] private Sprite smallDamageSprite;
    [SerializeField] private Sprite bigDamageSprite;

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
                break;
            case Status.BigDamage:
                MyStatus = Status.BigDamage;
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
}
