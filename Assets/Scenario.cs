using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using common_scripts;
using UnityEngine;

public class Scenario
{
    private readonly List<Func<Task>> actions = new List<Func<Task>>();
    public int currentAction { get; private set; } = -1;
    public int currentCheckpoint { get; private set; } = -1;

    private HashSet<GameObject> liveEnemies = new HashSet<GameObject>();
    
    private AsyncAutoResetEvent enemyAllDestroyEvent = new AsyncAutoResetEvent();
    
    public Scenario Spawn(Func<GameObject> factoryMethod)
    {
        actions.Add(async () =>
        {
            GameObject gameObject = factoryMethod.Invoke();
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
        actions.Add(async () => { currentCheckpoint = idx; });
        return this;
    }

    public Scenario WaitFor(Task task)
    {
        actions.Add(() => task);
        return this;
    }

    public Scenario WaitForAllEnemyDestroyed()
    {
        actions.Add(async () =>
        {
            await enemyAllDestroyEvent.WaitAsync();
        });
        return this;
    }

    public async void Run(int checkpointIdx)
    {
        for (currentAction = checkpointIdx + 1; currentAction < actions.Count; ++currentAction)
            await actions[currentAction].Invoke();
    }

    public void OnEnemyDestroy(GameObject enemy)
    {
        liveEnemies.Remove(enemy);
        if (liveEnemies.Count == 0)
        {
            enemyAllDestroyEvent.Set();
        }
    }
}