using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiWave : MonoBehaviour
{
    [SerializeField] Transform limits;

    EnemyWave[] enemyWaves;
    int currentWave = 0;

    void Awake()
    {
        enemyWaves = GetComponentsInChildren<EnemyWave>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (EnemyWave e in enemyWaves)
        {
            e.gameObject.SetActive(false);
            e.onWaveFinished.AddListener(OnWaveFinished);
        }
        limits.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        enemyWaves[currentWave].gameObject.SetActive(true);
    }

    void OnWaveFinished()
    {
        enemyWaves[currentWave].gameObject.SetActive(false);
        currentWave++;
        if (currentWave < enemyWaves.Length)
        {
            enemyWaves[currentWave].gameObject.SetActive(true);
        }
        else
        {
            limits.gameObject.SetActive(false);
        }
    }
}
