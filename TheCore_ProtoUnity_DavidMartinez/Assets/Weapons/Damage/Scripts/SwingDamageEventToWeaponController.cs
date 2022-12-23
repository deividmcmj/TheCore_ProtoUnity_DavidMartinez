using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingDamageEventToWeaponController : MonoBehaviour
{
    WeaponController weaponController;

    void Awake()
    {
        weaponController = GetComponentInParent<WeaponController>();
    }

    void SwingDamageStart()
    {
        weaponController.OnSwingDamageStart();
    }

    void SwingDamageEnd()
    {
        weaponController.OnSwingDamageEnd();
    }
}
