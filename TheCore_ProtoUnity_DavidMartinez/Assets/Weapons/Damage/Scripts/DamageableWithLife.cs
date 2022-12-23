using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageableWithLife : MonoBehaviour, IDamageable
{
    [SerializeField] float life;
    [SerializeField] public UnityEvent onDeath;
    [SerializeField] public UnityEvent<float> onChangeLifeNormalized;

    bool alreadyDead = false;
    float lifeAtStart;

    // Start is called before the first frame update
    void Start()
    {
        lifeAtStart = life;
    }

    void IDamageable.NotifyHit(float damage)
    {
        life -= damage;
        onChangeLifeNormalized.Invoke(life / lifeAtStart);
        if (life <= 0.0f)
        {
            if (!alreadyDead)
            {
                onDeath.Invoke();
                alreadyDead = true;
                Destroy(gameObject);
            }
        }
    }

    UnityEvent IDamageable.GetDeathEvent()
    {
        return onDeath;
    }
}
