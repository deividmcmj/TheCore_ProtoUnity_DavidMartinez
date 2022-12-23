using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCopy : MonoBehaviour
{
    [SerializeField] public Transform transformToCopyFrom;
    [SerializeField] public Vector3 localRelativePosition;
    [SerializeField] public Quaternion relativeRotation;

    void LateUpdate()
    {
        transform.position = transformToCopyFrom.TransformPoint(localRelativePosition);
        transform.rotation = transformToCopyFrom.rotation * relativeRotation;
    }
}
