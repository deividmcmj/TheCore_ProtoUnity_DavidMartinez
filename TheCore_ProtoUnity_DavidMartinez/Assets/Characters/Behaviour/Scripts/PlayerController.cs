using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement info")]
    [SerializeField] float speed = 10.0f;

    [Header("Inputs")]
    [SerializeField] InputAction moveForward;
    [SerializeField] InputAction moveBackwards;
    [SerializeField] InputAction moveLeft;
    [SerializeField] InputAction moveRight;
    [SerializeField] InputAction run;
    [SerializeField] InputAction shoot;
    [SerializeField] InputAction swing;
    [SerializeField] InputAction nextWeapon;
    [SerializeField] InputAction previousWeapon;

    [Header("Weapon info")]
    [SerializeField] int maxUses = 3;
    [SerializeField] float swordUsesChargeTime = 5.0f;

    public UnityEvent<int, int> onUseSword;

    CharacterController characterController;
    float velocityMultiplier = 0.5f;
    float verticalSpeed = 0.0f;
    float gravity = -9.81f;
    float lastSwordUseTime;
    Camera mainCamera;
    bool performSingleFireAttack = false;
    bool oldPerformContinuousAttack = false;
    bool performContinuousAttack = false;
    bool performMeleeAttack = false;
    WeaponController weaponController;
    int uses;

    void OnEnable()
    {
        moveForward.Enable();
        moveBackwards.Enable();
        moveLeft.Enable();
        moveRight.Enable();
        run.Enable();
        shoot.Enable();
        swing.Enable();
        nextWeapon.Enable();
        previousWeapon.Enable();
    }

    void OnDisable()
    {
        moveForward.Disable();
        moveBackwards.Disable();
        moveLeft.Disable();
        moveRight.Disable();
        run.Disable();
        shoot.Disable();
        swing.Disable();
        nextWeapon.Disable();
        previousWeapon.Disable();
    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        weaponController = GetComponent<WeaponController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lastSwordUseTime = 0.0f;
        mainCamera = Camera.main;
        uses = maxUses;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localMovement = ReadInputs();

        ApplyMovement(localMovement);

        UpdateWeaponUse();

        UpdateWeaponChange();

        if (Time.time - lastSwordUseTime > swordUsesChargeTime)
        {
            uses = maxUses;
            onUseSword.Invoke(uses, maxUses);
        }
    }

    Vector3 ReadInputs()
    {
        Vector3 localMovement = Vector3.zero;
        velocityMultiplier = run.IsPressed() ? 1.0f : 0.5f;

        if (moveForward.IsPressed())
        {
            localMovement += Vector3.forward;
        }
        if (moveBackwards.IsPressed())
        {
            localMovement += Vector3.back;
        }
        if (moveLeft.IsPressed())
        {
            localMovement += Vector3.left;
        }
        if (moveRight.IsPressed())
        {
            localMovement += Vector3.right;
        }
        localMovement.Normalize();

        performSingleFireAttack = shoot.triggered;
        performMeleeAttack = swing.triggered;
        performContinuousAttack = shoot.IsPressed();

        return localMovement;
    }

    void ApplyMovement(Vector3 localMovement)
    {
        Vector3 worldMovement = mainCamera.transform.TransformDirection(localMovement);
        float movementMagnitude = worldMovement.magnitude;
        Vector3 projectedWorldMovement = Vector3.ProjectOnPlane(worldMovement, Vector3.up);
        projectedWorldMovement = projectedWorldMovement.normalized * movementMagnitude;
        Vector3 movementOnPlane = projectedWorldMovement * speed * velocityMultiplier * Time.deltaTime;

        verticalSpeed += gravity * Time.deltaTime;
        Vector3 verticalVelocity = Vector3.up * verticalSpeed * Time.deltaTime;

        characterController.Move(movementOnPlane + verticalVelocity);

        if (characterController.isGrounded)
        {
            verticalSpeed = 0.0f;
        }
    }

    private void UpdateWeaponUse()
    {
        if (performSingleFireAttack && weaponController.GetCurrentWeaponUseMode() == FireWeaponBase.UseMode.Shot)
        {
            performSingleFireAttack = false;
            weaponController?.Shoot();
        }

        if (performMeleeAttack && uses > 0)
        {
            performMeleeAttack = false;
            weaponController?.Swing();
            uses--;
            onUseSword.Invoke(uses, maxUses);
            lastSwordUseTime = Time.time;
        }
        else
        {
            performMeleeAttack = false;
        }

        if (weaponController.GetCurrentWeaponUseMode() == FireWeaponBase.UseMode.ContinuousShot && oldPerformContinuousAttack != performContinuousAttack)
        {
            if (performContinuousAttack)
            {
                weaponController?.StartShooting();
            }
            else
            {
                weaponController?.StopShooting();
            }
        }

        oldPerformContinuousAttack = performContinuousAttack;
    }

    private void UpdateWeaponChange()
    {
        if (nextWeapon.triggered)
        {
            weaponController.SelectNextWeapon();
        }
        if (previousWeapon.triggered)
        {
            weaponController.SelectPreviousWeapon();
        }
    }
}
