using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstaKillDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] UnityEvent onDeath;

    bool alreadyDead = false;

    void IDamageable.NotifyHit(float damage)
    {
        if (!alreadyDead)
        {
            onDeath.Invoke();
            alreadyDead = true;
            Destroy(gameObject);
        }
    }

    UnityEvent IDamageable.GetDeathEvent()
    {
        return onDeath;
    }
}
