using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargeting : MonoBehaviour
{
    public enum TargetingMode
    {
        AimForward,
        AimAtTarget,
        AimAtCursor
    }
    [SerializeField] TargetingMode targetingMode = TargetingMode.AimForward;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        switch (targetingMode)
        {
            case TargetingMode.AimForward:
                UpdateAimForward();
                break;
        }
    }

    void UpdateAimForward()
    {
        Quaternion newRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up));
        transform.rotation = newRotation;
    }
}
