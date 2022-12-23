using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWave : MonoBehaviour
{
    public UnityEvent onWaveFinished;

    EnemyController[] enemies;
    int deathEnemies = 0;

    void Awake()
    {
        enemies = GetComponentsInChildren<EnemyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (EnemyController e in enemies)
        {
            e.gameObject.GetComponent<IDamageable>()?.GetDeathEvent()?.AddListener(OnDeath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDeath()
    {
        deathEnemies++;
        if (deathEnemies == enemies.Length)
        {
            onWaveFinished.Invoke();
        }
    }
}
