using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using common_scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenario
{
    private readonly List<Func<Task>> actions = new List<Func<Task>>();

    private readonly AsyncAutoResetEvent enemyAllDestroyEvent = new AsyncAutoResetEvent();

    private readonly HashSet<GameObject> liveEnemies = new HashSet<GameObject>();
    public int currentAction { get; private set; } = -1;
    public int currentCheckpoint { get; private set; } = -1;

    public Scenario Spawn(Func<GameObject> factoryMethod)
    {
        actions.Add(async () =>
        {
            var gameObject = factoryMethod.Invoke();
            gameObject.GetComponent<ScenarioNotify>().scenario = this;
            liveEnemies.Add(gameObject);
        });
        return this;
    }

    public Scenario Spawn(ScenarioFactory.EnemyBuilder builder)
    {
        return Spawn(builder.build);
    }

    public Scenario Checkpoint()
    {
        var idx = actions.Count;
        actions.Add(async () =>
        {
            currentCheckpoint = idx;
            Debug.Log("current checkpoint: " + currentCheckpoint);
        });
        return this;
    }

    public Scenario WaitFor(Task task)
    {
        actions.Add(() => task);
        return this;
    }

    public Scenario WaitForAllEnemyDestroyed()
    {
        actions.Add(async () => { await enemyAllDestroyEvent.WaitAsync(); });
        return this;
    }

    public Scenario WaitForSeconds(float seconds)
    {
        actions.Add(async () => { await Task.Delay((int) (seconds * 1000)); });
        return this;
    }

    public Scenario End()
    {
        actions.Add(async () =>
        {
            GameStatus.Instance.scenario = "end";
            SceneManager.LoadScene("avg/avg");
        });
        return this;
    }

    public async void Run(int checkpointIdx, CancellationToken cancellationToken)
    {
        Debug.Log("running scenario at: " + checkpointIdx);
        for (currentAction = checkpointIdx + 1; currentAction < actions.Count; ++currentAction)
        {
            if (cancellationToken.IsCancellationRequested) break;
            if (GameStatus.Instance.isShuttingDown) break;
            await actions[currentAction].Invoke();
        }
    }

    public void OnEnemyDestroy(GameObject enemy)
    {
        liveEnemies.Remove(enemy);
        if (liveEnemies.Count == 0) enemyAllDestroyEvent.Set();
    }
}