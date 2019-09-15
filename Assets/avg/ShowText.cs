using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public const int AUDIO_TYPE_TYPE = 0;
    public const int AUDIO_TYPE_SAY = 1;
    
    private string text = "";
    private Text textComponent;
    private double textSpeed;
    private Action textFinishCallback;

    private double textTime;
    public AudioSource typeAudio;
    public AudioSource sayAudio;
    private int audioType;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
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

    public void Show(string text, double speed = 10.0f, Action finishCallback = null, int audioType = AUDIO_TYPE_TYPE)
    {
        this.text = text;
        textTime = 0;
        textSpeed = speed;
        textFinishCallback = finishCallback;
        this.audioType = audioType;
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
            var dText = newText.Substring(textComponent.text.Length);
            AudioSource audioSource = audioType == AUDIO_TYPE_SAY ? sayAudio : typeAudio;
            if (dText.Trim().Length > 0 && (!audioSource.isPlaying || audioSource.time > 0.05))
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