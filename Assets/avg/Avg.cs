using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using common_scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Coroutine = MoonSharp.Interpreter.Coroutine;

public class Avg : MonoBehaviour
{
    private Coroutine coroutine;
    public OptionGroup optionGroup;
    
    private bool textAuto;
    public GameObject textField;

    private bool isOption = false;

    /**
     * speed: char per second
     */
    private void Say(string sth, double speed = 10.0f, bool auto = false)
    {
        textAuto = auto;
        textField.GetComponent<ShowText>().Show(sth, speed, () =>
        {
            if (textAuto) coroutine.Resume();
        });
        isOption = false;
    }

    private void Choose(string text, IList<IDictionary> options)
    {
        double speed = 1000000.0f;
        textAuto = false;
        textField.GetComponent<ShowText>().Show(text, speed, () =>
        {
            if (textAuto) coroutine.Resume();
        });
        
        optionGroup.InitOptions(
            options.Select(option => option["info"] as string).ToList(),
            arg =>
            {
                optionGroup.ClearOptions();
            });
        isOption = true;
    }

    private int FailCount()
    {
        return GameStatus.Instance.failCount;
    }

    private void ResumeBattle(BwBuff bwBuff)
    {
//        Debug.Log(bwBuff);
        var gameStatus = GameStatus.Instance;
        if (bwBuff > 0)
        {
            gameStatus.bwBuffs = new[]{bwBuff};
            gameStatus.bwDelay = 0.1f;
        }

        SceneManager.LoadScene("battle", LoadSceneMode.Single);
    }

    // Use this for initialization
    private void Start()
    {
        
        MoonSharpManager.RegisterFunc("Say", (Action<string, double, bool>) Say);
        MoonSharpManager.RegisterFunc("Choose", (Action<string, IList<IDictionary>>) Choose);
        MoonSharpManager.RegisterFunc("FailCount", (Func<int>) FailCount);
        MoonSharpManager.RegisterFunc("ResumeBattle", (Action<BwBuff>) ResumeBattle);
        MoonSharpManager.RegisterFunc("QuitGame", (Action) Application.Quit);
        coroutine = MoonSharpManager.RunCoroutine(GameStatus.Instance.scenario);
        coroutine.Resume();
    }

    // Update is called once per frame
    private void Update()
    {
        var keyConfig = KeyConfig.Instance;
        foreach (var keyCode in keyConfig.avgConfirm)
        {
            if (Input.GetKeyDown(keyCode))
            {
                if (isOption)
                {
                    coroutine.Resume(optionGroup.GetCurrentIdx() + 1);
                }
                else
                {
                    coroutine.Resume();
                }
            }
        }
    }
}