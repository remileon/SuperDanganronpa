using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using common_scripts;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UIElements;
using Coroutine = MoonSharp.Interpreter.Coroutine;
using Image = UnityEngine.UI.Image;

public class Avg : MonoBehaviour
{
    private Coroutine coroutine;
    public ShowImg showImg;

    private bool isOption;
    public OptionGroup optionGroup;
    private ShowText showText;

    private bool textAuto;
    public GameObject textField;

    /**
     * speed: char per second
     */
    private void Say(string sth, double speed = 10.0f, bool auto = false, int audioType = ShowText.AUDIO_TYPE_SAY)
    {
        textAuto = auto;
        showText.Show(sth, speed, () =>
        {
            if (textAuto) coroutine.Resume();
        }, audioType);
        isOption = false;
    }

    private void Choose(string text, IList<IDictionary> options)
    {
        double speed = 1000000.0f;
        textAuto = false;
        showText.Show(text, speed, () =>
        {
            if (textAuto) coroutine.Resume();
        });

        optionGroup.InitOptions(
            options.Select(option => option["info"] as string).ToList());
        isOption = true;
    }

    private void ShowImg(int idx, float duration)
    {
        showImg.Show(idx, duration, () => coroutine.Resume());
    }

    private void HideImg()
    {
        showImg.Hide();
    }

    private int FailCount()
    {
        return GameStatus.Instance.failCount;
    }

    private void ResumeBattle(BwBuff bwBuff)
    {
        var gameStatus = GameStatus.Instance;
        if (bwBuff > 0)
        {
            gameStatus.bwBuffs = new[] {bwBuff};
            gameStatus.bwDelay = 0.1f;
        }

        if (bwBuff == 0)
            gameStatus.endFlag = GameStatus.EndFlagFuture;
        else if (bwBuff == BwBuff.Hope)
            gameStatus.endFlag = GameStatus.EndFlagHope;
        else if (bwBuff == BwBuff.Despair)
            gameStatus.endFlag = GameStatus.EndFlagDespair;
        else if (bwBuff == BwBuff.Negative) gameStatus.endFlag = GameStatus.EndFlagNegative;

        SceneFader.Instance.FadeToBattle();
    }

    private int EndFlag()
    {
        var gameStatus = GameStatus.Instance;
        return gameStatus.endFlag;
    }

    // Use this for initialization
    private void Start()
    {
        HideImg();
        showText = textField.GetComponent<ShowText>();

        MoonSharpManager.RegisterFunc("Say", (Action<string, double, bool, int>) Say);
        MoonSharpManager.RegisterFunc("Choose", (Action<string, IList<IDictionary>>) Choose);
        MoonSharpManager.RegisterFunc("FailCount", (Func<int>) FailCount);
        MoonSharpManager.RegisterFunc("ResumeBattle", (Action<BwBuff>) ResumeBattle);
        MoonSharpManager.RegisterFunc("QuitGame", (Action) Application.Quit);
        MoonSharpManager.RegisterFunc("EndFlag", (Func<int>) EndFlag);
        MoonSharpManager.RegisterFunc("ShowImg", (Action<int, float>) ShowImg);
        MoonSharpManager.RegisterFunc("HideImg", (Action) HideImg);
        coroutine = MoonSharpManager.RunCoroutine(GameStatus.Instance.scenario);

        Invoke(nameof(StartAvg), 1);
    }

    public void StartAvg()
    {
        coroutine.Resume();
    }

    // Update is called once per frame
    private void Update()
    {
        if (InputController.Instance.confirmAction.triggered)
        {
            if (isOption)
            {
                if (optionGroup.protectTime <= 0) {
                    optionGroup.ClearOptions();
                    if (coroutine.State == CoroutineState.Suspended) {
                        coroutine.Resume(optionGroup.GetCurrentIdx() + 1);
                    }
                }
            }
            else
            {
                if (!showText.isShowing() && !showImg.isShowing)
                    if (coroutine.State == CoroutineState.Suspended)
                    {
                        coroutine.Resume();
                    }
            }
        }
    }
}