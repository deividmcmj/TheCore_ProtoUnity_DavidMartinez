using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponByOverlap : MeleeWeaponBase
{
    [SerializeField] float weaponRange = 1.0f;
    [SerializeField] LayerMask damageableLayerMask = Physics.DefaultRaycastLayers;

    public override void Swing()
    {
        
    }

    public override void SwingDamageStart()
    {
        Collider[] colliders = Physics.OverlapSphere(ownerHalfHeight.transform.position + (ownerHalfHeight.forward * weaponRange / 2.0f), weaponRange / 2.0f, damageableLayerMask);

        foreach (Collider c in colliders)
        {
            c.GetComponent<IDamageable>()?.NotifyHit();
        }
    }

    public override void SwingDamageEnd()
    {
        
    }
}
