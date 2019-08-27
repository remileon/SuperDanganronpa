using System.Collections;
using System.Collections.Generic;
using System.Linq;
using common_scripts;
using UnityEngine;

public class Director : MonoBehaviour {
    public GameObject bw;
    public EnemyWaves[] enemyWaves;

    private int curWave = 0;
    private GameObject[] curEnemies = null;

    void Awake()
    {
        float delay = GameStatus.Instance.bwDelay;
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
        var gameStatus = GameStatus.Instance;
        var bwBuffs = gameStatus.bwBuffs;
        // negative
        if (bwBuffs.Contains(BwBuff.Negative))
        {
            return;
        }
        // hope
        gameStatus.bwLife = bwBuffs.Contains(BwBuff.Hope) ? 15 : 3;
        // despair
        if (bwBuffs.Contains(BwBuff.Despair))
        {
            // todo: attach more weapons
        }
        // negative
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
