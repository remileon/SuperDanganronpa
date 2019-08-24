using System;
using System.Collections;
using System.Collections.Generic;
using common_scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Coroutine = MoonSharp.Interpreter.Coroutine;

public class Test : MonoBehaviour
{
    public GameObject textField;
    public GameObject optionPrefab;
    public int optionMargin;
    
    private Coroutine coroutine;

    private double textTime = 0;
    private double textSpeed = 0;
    private string text = "";
    private bool textAuto = false;
    private Text textComponent;

    private IList<IDictionary> options;

    /**
     * speed: char per second
     */
    private void Say(string sth, double speed = 10.0f, bool auto = false)
    {
        Debug.Log(speed);
        this.text = sth;
        this.textTime = 0;
        this.textSpeed = speed;
        this.textAuto = auto;
        UpdateText();
    }

    private void Choose(string text, IList<IDictionary> options)
    {
        double speed = 1000000.0f;
        this.text = text;
        this.textTime = 1;
        this.textSpeed = speed;
        this.textAuto = false;
        UpdateText();

        this.options = options;
        Debug.Log(JsonUtility.ToJson(options));
        ShowChooses(options);
    }

    // Use this for initialization
    private void Start()
    {
        textComponent = textField.GetComponent<Text>();
        MoonSharpManager.RegisterFunc("Say", (Action<string, double, bool>) Say);
        MoonSharpManager.RegisterFunc("Choose", (Action<string, IList<IDictionary>>) Choose);
        coroutine = MoonSharpManager.RunCoroutine("test");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
//            Debug.Log("enter");
            coroutine.Resume(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            coroutine.Resume(2);
        }

        textTime += Time.deltaTime;
        UpdateText();
    }

    private void UpdateText()
    {
        int cur = (int) (textSpeed * textTime);
        textComponent.text = text.Substring(0, Math.Min(cur, text.Length));
        if (textAuto && cur > text.Length)
        {
            coroutine.Resume();
        }
    }

    private void ShowChooses(IList<IDictionary> options)
    {
        for (var i = 0; i < options.Count; i++)
        {
            IDictionary option = options[i];
            string info = option["info"] as string;
            GameObject optionGameObject = Instantiate(optionPrefab, gameObject.transform, false);

            var rectTransform = optionGameObject.GetComponent<RectTransform>();
            Debug.Log((options.Count - i - 1) * (optionMargin + rectTransform.rect.height));
            var rectTransformAnchoredPosition = rectTransform.anchoredPosition;
            rectTransformAnchoredPosition.y += (options.Count - i - 1) * (optionMargin + rectTransform.rect.height);
            rectTransform.anchoredPosition = rectTransformAnchoredPosition;

            optionGameObject.GetComponentInChildren<TextMeshProUGUI>().text = info;
        }

    }
}