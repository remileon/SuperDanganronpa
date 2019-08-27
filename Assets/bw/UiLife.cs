using System.Collections.Generic;
using UnityEngine;

public class UiLife : MonoBehaviour
{
    [SerializeField] private GameObject lifePrefab;

    private List<GameObject> lifes = new List<GameObject>();

    public void Init(int lifeNum)
    {
        while (lifeNum-- > 0)
        {
            Debug.Log(lifeNum);
            var life = Instantiate(lifePrefab, gameObject.transform, false);
            lifes.Add(life);
        }
    }

    public void LoseLife()
    {
        if (lifes.Count <= 0) return;

        var idx = lifes.Count - 1;

        var life = lifes[idx];
        Destroy(life);
        lifes.RemoveAt(idx);
    }
}