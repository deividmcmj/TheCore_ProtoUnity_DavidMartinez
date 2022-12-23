using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.0f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in colliders)
        {
            IDamageable damageable = c.GetComponent<IDamageable>();
            if (damageable != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, c.transform.position - transform.position, out hit, radius, layerMask))
                {
                    if (hit.collider == c)
                    {
                        damageable.NotifyHit();
                    }
                }
            }
        }

        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
