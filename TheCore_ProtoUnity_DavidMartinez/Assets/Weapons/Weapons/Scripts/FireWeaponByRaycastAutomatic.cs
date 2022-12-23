using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponByRaycastAutomatic : FireWeaponByRaycast
{
    [SerializeField] float cadence = 5.0f;

    bool isShooting = false;
    float timeSinceLastShot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting && (Time.time - timeSinceLastShot) > (1.0f / cadence))
        {
            timeSinceLastShot = Time.time;
            Shoot();
        }
    }

    public override void StartShooting()
    {
        isShooting = true;
    }

    public override void StopShooting()
    {
        isShooting = false;
    }
}
