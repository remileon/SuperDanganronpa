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
    public int optionMargin;
    public GameObject optionPrefab;

    private IList<IDictionary> options;

    private bool textAuto;
    public GameObject textField;

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
    }

    private void Choose(string text, IList<IDictionary> options)
    {
        double speed = 1000000.0f;
        textAuto = false;
        textField.GetComponent<ShowText>().Show(text, speed, () =>
        {
            if (textAuto) coroutine.Resume();
        });

        this.options = options;
//        Debug.Log(JsonUtility.ToJson(options));
        ShowChooses(options);
    }

    private int FailCount()
    {
        return GameStatus.Instance.failCount;
    }

    private void ResumeBattle(BwBuff bwBuff)
    {
        Debug.Log(bwBuff);
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
        coroutine = MoonSharpManager.RunCoroutine(GameStatus.Instance.scenario);
        coroutine.Resume();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            //            Debug.Log("enter");
            coroutine.Resume(1);

        if (Input.GetKeyDown(KeyCode.Space)) coroutine.Resume(2);
    }

    private void ShowChooses(IList<IDictionary> options)
    {
        for (var i = 0; i < options.Count; i++)
        {
            var option = options[i];
            var info = option["info"] as string;
            var optionGameObject = Instantiate(optionPrefab, gameObject.transform, false);

            var rectTransform = optionGameObject.GetComponent<RectTransform>();
            Debug.Log((options.Count - i - 1) * (optionMargin + rectTransform.rect.height));
            var rectTransformAnchoredPosition = rectTransform.anchoredPosition;
            rectTransformAnchoredPosition.y += (options.Count - i - 1) * (optionMargin + rectTransform.rect.height);
            rectTransform.anchoredPosition = rectTransformAnchoredPosition;

            optionGameObject.GetComponentInChildren<TextMeshProUGUI>().text = info;
        }
    }
}