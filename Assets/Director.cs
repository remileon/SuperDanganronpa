﻿using System.Linq;
using System.Threading;
using common_scripts;
using UnityEngine;

public class Director : MonoBehaviour
{
    private readonly int curWave = 0;
    public GameObject bw;

    private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private GameObject[] curEnemies;
    public EnemyWaves[] enemyWaves;
    private Scenario scenario;
    public GameObject despairWeapon;

    private void Awake()
    {
        var delay = GameStatus.Instance.bwDelay;
        Invoke("SummonBw", delay);
        scenario = gameObject.GetComponent<ScenarioFactory>()
            .create();

        Invoke("RunScenario", 1);
    }

    private void Update()
    {
    }

    private void RunScenario()
    {
        scenario.Run(GameStatus.Instance.enemyProgress, cancellationTokenSource.Token);
    }

    private void SummonBw()
    {
        var gameStatus = GameStatus.Instance;
        var bwBuffs = gameStatus.bwBuffs;
        // negative
        if (bwBuffs.Contains(BwBuff.Negative)) return;
        // hope
        gameStatus.bwLife = bwBuffs.Contains(BwBuff.Hope) ? 18 : 3;
        if (gameStatus.isDebugging)
        {
            gameStatus.bwLife = 1;
        }
        var gameObject = Instantiate(bw);
        // despair
        if (bwBuffs.Contains(BwBuff.Despair))
        {
            var weaponSlot = gameObject.GetComponent<AttachWeapons>().weaponSlots[0];
            weaponSlot.weapon = despairWeapon;
        }

        gameObject.GetComponent<Bw>().director = this;
    }

    public void Cut()
    {
        GameStatus.Instance.enemyProgress = scenario.currentCheckpoint;
        cancellationTokenSource.Cancel();
    }

    private bool ShouldCreateNextWave()
    {
        if (curEnemies == null) return false;
        if (curWave >= enemyWaves.Length - 1) return false;
        foreach (var enemy in curEnemies)
            if (enemy != null)
                return false;
        return true;
    }

    private void CreateFirstWave()
    {
        CreateWave(0);
    }

    private void CreateWave(int curWave)
    {
        curEnemies = new GameObject[enemyWaves[curWave].enemies.Length];
        for (var i = 0; i < enemyWaves[curWave].enemies.Length; ++i)
        {
            var enemy = enemyWaves[curWave].enemies[i];
            var newEnemy = Instantiate(enemy);
            curEnemies[i] = newEnemy;
        }
    }
}