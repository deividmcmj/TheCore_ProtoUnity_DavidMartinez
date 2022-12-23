using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireWeaponBase : WeaponBase
{
    [Header("Weapon info")]
    [SerializeField] protected Transform shootPoint;

    [Header("Debug")]
    [SerializeField] bool debugShoot;
    [SerializeField] bool debugStartShooting;
    [SerializeField] bool debugStopShooting;
    [SerializeField] bool debugReload;

    void OnValidate()
    {
        if (debugShoot)
        {
            debugShoot = false;
            Shoot();
        }
    }

    public abstract void Shoot();
}
