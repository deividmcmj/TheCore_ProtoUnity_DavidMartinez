using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeaponBase : WeaponBase
{
    [SerializeField] protected Transform ownerHalfHeight;

    public abstract void Swing();
    public abstract void SwingDamageStart();
    public abstract void SwingDamageEnd();
}
