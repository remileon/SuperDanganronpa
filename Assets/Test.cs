using System;
using common_scripts;
using UnityEngine;
using UnityEngine.UI;
using Coroutine = MoonSharp.Interpreter.Coroutine;

public class Test : MonoBehaviour
{
    private Coroutine coroutine;
    public GameObject textField;

    private double textTime = 0;
    private double textSpeed = 0;
    private string text = "";
    private bool textAuto = false;
    private Text textComponent;

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

    // Use this for initialization
    private void Start()
    {
        textComponent = textField.GetComponent<Text>();
        MoonSharpManager.RegisterFunc("Say", (Action<string, double, bool>) Say);
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
}