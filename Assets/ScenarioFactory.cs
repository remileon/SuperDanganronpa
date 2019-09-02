using TMPro;
using UnityEngine;

public class ScenarioFactory : MonoBehaviour
{
    [SerializeField] private GameObject baseEnemy;

    public Scenario create()
    {
        var scenario = new Scenario();
        scenario
            .Spawn(() => { return enemy("111"); })
            .WaitForAllEnemyDestroyed()
            .Spawn(() => { return enemy("22222222222222222222"); });
        return scenario;
    }

    private GameObject enemy(string text)
    {
        var enemy = Instantiate(baseEnemy);
        enemy.GetComponent<TextMeshPro>().text = text;
        return enemy;
    }
}