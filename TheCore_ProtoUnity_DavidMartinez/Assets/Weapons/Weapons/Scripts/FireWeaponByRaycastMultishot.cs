using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponByRaycastMultishot : FireWeaponByRaycast
{
    [SerializeField] int shardsPerShot = 140;

    public override void Shoot()
    {
        for (int i=0; i<shardsPerShot; i++)
        {
            base.Shoot();
        }

        lastShotTime = Time.time;
    }
}
