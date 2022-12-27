using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireWeaponBase : WeaponBase
{
    public enum UseMode
    {
        Shot,
        ContinuousShot
    }

    [Header("Weapon info")]
    [SerializeField] public UseMode useMode;
    [SerializeField] public RuntimeAnimatorController animatorForWeapon;
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
        if (debugStartShooting)
        {
            debugStartShooting = false;
            StartShooting();
        }
        if (debugShoot)
        {
            debugShoot = false;
            StopShooting();
        }
    }

    public abstract void Shoot();
    public virtual void StartShooting() { }
    public virtual void StopShooting() { }
}
