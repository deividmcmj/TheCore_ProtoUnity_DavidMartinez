using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponByRaycast : FireWeaponBase
{
    [SerializeField] float maxHorizontalSpreadAngle = 0.0f;
    [SerializeField] float maxVerticalSpreadAngle = 0.0f;
    [SerializeField] float damage = 1.0f;
    [SerializeField] float range = Mathf.Infinity;
    [SerializeField] LayerMask shotLayerMask = Physics.DefaultRaycastLayers;

    public override void Shoot()
    {
        float horizontalSpreadAngle = Random.Range(-maxHorizontalSpreadAngle / 2.0f, maxHorizontalSpreadAngle / 2.0f);
        float verticalSpreadAngle = Random.Range(-maxVerticalSpreadAngle / 2.0f, maxVerticalSpreadAngle / 2.0f);
        Quaternion horizontalSpread = Quaternion.AngleAxis(horizontalSpreadAngle, shootPoint.up);
        Quaternion verticalSpread = Quaternion.AngleAxis(verticalSpreadAngle, shootPoint.right);

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, horizontalSpread * verticalSpread * shootPoint.forward, out hit, range, shotLayerMask))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            damageable?.NotifyHit(damage);
        }
    }
}
