using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionGroup : MonoBehaviour
{
    private int currentIdx;
    public GameObject optionPrefab;
    private List<GameObject> options = new List<GameObject>();
    private List<Option> optionScripts = new List<Option>();
    public float protectTime { get; private set; } = 0f; 

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        protectTime -= Time.deltaTime;
        if (options.Count <= 0) return;
        var oldCurrentIdx = currentIdx;
        var moveAction = InputController.Instance.moveAction;
        if (moveAction.triggered)
        {
            if (moveAction.ReadValue<Vector2>().y < -0.5f)
                if (currentIdx < options.Count - 1)
                    ++currentIdx;

            if (moveAction.ReadValue<Vector2>().y > 0.5f)
                if (currentIdx > 0)
                    --currentIdx;
        }

        if (oldCurrentIdx != currentIdx)
        {
            optionScripts[oldCurrentIdx].SetHovered(false);
            optionScripts[currentIdx].SetHovered(true);
        }
    }

    public void InitOptions(List<string> infos)
    {
        if (infos.Count <= 0) return;
        options = new List<GameObject>();
        optionScripts = new List<Option>();
        foreach (var info in infos)
        {
            var optionGameObject = Instantiate(optionPrefab, gameObject.transform, false);
            optionGameObject.GetComponentInChildren<TextMeshProUGUI>().text = info;

            options.Add(optionGameObject);
            optionScripts.Add(optionGameObject.GetComponent<Option>());
        }

        currentIdx = 0;
        optionScripts[currentIdx].SetHovered(true);
        protectTime = 0.5f;
    }

    public void ClearOptions()
    {
        foreach (var option in options) Destroy(option);

        options = new List<GameObject>();
        optionScripts = new List<Option>();
    }

    public int GetCurrentIdx()
    {
        return currentIdx;
    }
}