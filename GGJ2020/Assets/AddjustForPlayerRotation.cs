﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddjustForPlayerRotation : MonoBehaviour
{
    [SerializeField] Transform parentTransform;

    private void FixedUpdate()
    {
        if (parentTransform.localScale.x < 0)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

    }
}
