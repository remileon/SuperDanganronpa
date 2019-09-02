using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioNotify : MonoBehaviour
{
    public Scenario scenario { set; private get; }

    private void OnDestroy()
    {
        if (scenario != null)
        {
            scenario.OnEnemyDestroy(this.gameObject);
        }
    }
}
