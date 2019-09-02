using System.Linq;
using common_scripts;
using TMPro;
using UnityEngine;

public class Director : MonoBehaviour
{
    public GameObject bw;
    private GameObject[] curEnemies;

    private readonly int curWave = 0;
    public EnemyWaves[] enemyWaves;

    private void Awake()
    {
        var delay = GameStatus.Instance.bwDelay;
        Invoke("SummonBw", delay);
        var scenario = gameObject.GetComponent<ScenarioFactory>()
            .create();

        scenario.Run(-1);
    }

    private void Update()
    {
    }

    private void SummonBw()
    {
        var gameStatus = GameStatus.Instance;
        var bwBuffs = gameStatus.bwBuffs;
        // negative
        if (bwBuffs.Contains(BwBuff.Negative)) return;
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