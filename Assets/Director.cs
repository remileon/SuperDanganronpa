using System.Linq;
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

    private void Awake()
    {
        var delay = GameStatus.Instance.bwDelay;
        Invoke("SummonBw", delay);
        scenario = gameObject.GetComponent<ScenarioFactory>()
            .create();

        scenario.Run(GameStatus.Instance.enemyProgress, cancellationTokenSource.Token);
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
        if (gameStatus.isDebugging)
        {
            gameStatus.bwLife = 30;
        }
        // despair
        if (bwBuffs.Contains(BwBuff.Despair))
        {
            // todo: attach more weapons
        }
        // negative

        var gameObject = Instantiate(bw);
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