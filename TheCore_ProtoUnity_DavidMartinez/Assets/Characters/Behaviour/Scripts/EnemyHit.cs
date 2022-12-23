using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float coolDownBetweenHits = 2.0f;
    [SerializeField] float attackRange = 1.0f;
    [SerializeField] float damage = 1.0f;

    float lastHitTime;

    // Start is called before the first frame update
    void Start()
    {
        lastHitTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanHit() && TargetIsCloseEnough())
        {
            HitTarget();
        }
    }

    bool CanHit()
    {
        return Time.time - lastHitTime > coolDownBetweenHits;
    }

    bool TargetIsCloseEnough()
    {
        return target && Vector3.Distance(target.position, transform.position) < attackRange;
    }

    void HitTarget()
    {
        target?.GetComponent<IDamageable>()?.NotifyHit(damage);
        lastHitTime = Time.time;
    }
}
