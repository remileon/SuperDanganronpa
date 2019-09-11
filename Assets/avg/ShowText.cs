using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private string text = "";
    private Text textComponent;
    private double textSpeed;
    private Action textFinishCallback;

    private double textTime;
    private AudioSource audioSource;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        textTime += Time.deltaTime;
        UpdateText();
    }

    public void Show(string text, double speed = 10.0f, Action finishCallback = null)
    {
        this.text = text;
        textTime = 0;
        textSpeed = speed;
        textFinishCallback = finishCallback;
        UpdateText();
    }

    private void UpdateText()
    {
        if (textComponent.text.Length == text.Length)
        {
            return;
        }
        var cur = (int) (textSpeed * textTime);
        var newText = text.Substring(0, Math.Min(cur, text.Length));
        if (textComponent.text.Length < newText.Length)
        {
            if (!audioSource.isPlaying || audioSource.time > 0.05)
            {
                audioSource.Play();
            }
        }
        textComponent.text = newText;
        if (textComponent.text.Length == text.Length)
        {
            if (textFinishCallback != null)
            {
                textFinishCallback.Invoke();
            }
        }
    }
}