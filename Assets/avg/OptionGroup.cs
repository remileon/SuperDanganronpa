using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionGroup : MonoBehaviour
{
    public GameObject optionPrefab;

    private Action<int> callback;
    private List<GameObject> options = new List<GameObject>();
    private List<Option> optionScripts = new List<Option>();
    private int currentIdx;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (options.Count <= 0)
        {
            return;
        }
        int oldCurrentIdx = currentIdx;
        var keyConfig = KeyConfig.Instance;
        foreach (var keyCode in keyConfig.avgDown)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (currentIdx < options.Count - 1)
                {
                    ++currentIdx;
                }
            }   
        }
        foreach (var keyCode in keyConfig.avgUp)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (currentIdx > 0)
                { 
                    --currentIdx;
                }
            }
        }

        if (oldCurrentIdx != currentIdx)
        {
            optionScripts[oldCurrentIdx].SetHovered(false);
            optionScripts[currentIdx].SetHovered(true);
        }
        
        foreach (var keyCode in keyConfig.avgConfirm)
        {
            if (Input.GetKeyDown(keyCode))
            {
                callback.Invoke(currentIdx);
            }
        }
    }

    public void InitOptions(List<string> infos, Action<int> callback)
    {
        if (infos.Count <= 0)
        {
            return;
        }
        options = new List<GameObject>();
        optionScripts = new List<Option>();
        foreach (var info in infos)
        {
            var optionGameObject = Instantiate(optionPrefab, gameObject.transform, false);
            optionGameObject.GetComponentInChildren<TextMeshProUGUI>().text = info;
            
            options.Add(optionGameObject);
            optionScripts.Add(optionGameObject.GetComponent<Option>());
        }

        this.currentIdx = 0;
        this.callback = callback;
        optionScripts[currentIdx].SetHovered(true);
    }

    public void ClearOptions()
    {
        foreach (var option in options)
        {
            GameObject.Destroy(option);
        }

        options = new List<GameObject>();
        optionScripts = new List<Option>();
    }

    public int GetCurrentIdx()
    {
        return currentIdx;
    }
}
