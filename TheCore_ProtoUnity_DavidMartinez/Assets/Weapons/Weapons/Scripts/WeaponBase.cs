using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public enum UseMode
    {
        Shot,
        ContinuousShot,
        Swing
    }

    [Header("Weapon info")]
    [SerializeField] public RuntimeAnimatorController animatorForWeapon;
    [SerializeField] public UseMode useMode;
}
