using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponByInstantiation : FireWeaponBase
{
    [SerializeField] GameObject projectilePrefab;

    public override void Shoot()
    {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        canShoot.text = "No";
        canShoot.color = Color.red;
        lastShotTime = Time.time;
    }
}
