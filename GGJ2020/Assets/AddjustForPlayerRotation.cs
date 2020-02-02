using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddjustForPlayerRotation : MonoBehaviour
{
    [SerializeField] Transform parentTransform;

    private void FixedUpdate()
    {
        transform.localScale = new Vector3(parentTransform.localScale.x * -1, parentTransform.localScale.y, parentTransform.localScale.z);
    }
}
