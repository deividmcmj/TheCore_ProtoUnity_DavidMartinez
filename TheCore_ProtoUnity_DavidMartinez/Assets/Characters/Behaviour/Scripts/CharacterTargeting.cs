using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterTargeting : MonoBehaviour
{
    public enum TargetingMode
    {
        AimForward,
        AimAtTarget,
        AimAtCursor
    }
    [SerializeField] TargetingMode targetingMode = TargetingMode.AimForward;
    [SerializeField] LayerMask layerMaskCursorAimDirection = Physics.DefaultRaycastLayers;
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
            case TargetingMode.AimAtTarget:
                UpdateAimAtTarget();
                break;
            case TargetingMode.AimAtCursor:
                UpdateAimAtCursor();
                break;
        }
    }

    void UpdateAimForward()
    {
        Quaternion newRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up));
        transform.rotation = newRotation;
    }

    void UpdateAimAtTarget()
    {

    }

    void UpdateAimAtCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskCursorAimDirection))
        {
            Vector3 lookDirection = hit.point - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(lookDirection, Vector3.up));
            transform.rotation = newRotation;
        }
    }
}
