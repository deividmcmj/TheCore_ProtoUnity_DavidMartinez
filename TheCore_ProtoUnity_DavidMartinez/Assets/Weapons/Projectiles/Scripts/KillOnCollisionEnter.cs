using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnCollisionEnter : MonoBehaviour
{
    [SerializeField] GameObject prefabToInstantiateOnCollision;

    void OnCollisionEnter(Collision collision)
    {
        if (prefabToInstantiateOnCollision)
        {
            Instantiate(prefabToInstantiateOnCollision, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
