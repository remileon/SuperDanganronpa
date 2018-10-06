using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    public GameObject bw;
    public EnemyWaves[] enemyWaves;

    private int curWave = 0;
    private GameObject[] curEnemies = null;

    void Awake()
    {
        float delay = 4.15f;
        Invoke("SummonBw", delay);
        Invoke("CreateFirstWave", delay + 1);
    }

    void Update()
    {
        if (ShouldCreateNextWave())
        {
            ++curWave;
            CreateWave(curWave);
        }
    }

    void SummonBw()
    {
        Instantiate(bw);
    }

    bool ShouldCreateNextWave()
    {
        if (curEnemies == null)
        {
            return false;
        }
        if (curWave >= enemyWaves.Length - 1)
        {
            return false;
        }
        foreach (GameObject enemy in curEnemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }

    void CreateFirstWave()
    {
        CreateWave(0);
    }

    void CreateWave(int curWave)
    {
        curEnemies = new GameObject[enemyWaves[curWave].enemies.Length];
        for (int i = 0; i < enemyWaves[curWave].enemies.Length; ++i)
        {
            GameObject enemy = enemyWaves[curWave].enemies[i];
            GameObject newEnemy = Instantiate(enemy);
            curEnemies[i] = newEnemy;
        }
    }
}
