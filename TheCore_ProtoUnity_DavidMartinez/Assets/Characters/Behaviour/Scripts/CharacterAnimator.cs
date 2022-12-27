using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10.0f;

    Animator animator;
    Vector3 oldPosition;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        WeaponController weaponController = GetComponentInChildren<WeaponController>();
        if (weaponController)
        {
            weaponController.onShoot.AddListener(OnShoot);
            weaponController.onSwing.AddListener(OnSwing);
            weaponController.onChangeWeapon.AddListener(OnChangeWeapon);
        }
        oldPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldVelocity = (transform.position - oldPosition) / Time.deltaTime;
        Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);

        animator.SetFloat("ForwardVelocity", localVelocity.z / maxSpeed);
        animator.SetFloat("HorizontalVelocity", localVelocity.x / maxSpeed);

        oldPosition = transform.position;
    }

    void OnShoot()
    {
        animator.SetTrigger("Shoot");
    }

    void OnSwing()
    {
        animator.SetTrigger("Swing");
    }

    void OnChangeWeapon(FireWeaponBase weapon)
    {
        animator.runtimeAnimatorController = weapon.animatorForWeapon;
    }
}
