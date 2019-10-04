using System;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    public AudioSource loop;
    public AudioSource op1;

    public int status = 1;

    private bool isFadingOut = false;
    private AudioSource playing;
    private float maxVolume;
    private float duration;
    private float curTime;

    public static BgmController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

//        op1.time = 520;
        op1.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        if (status == 1)
        {
            if (Math.Abs(op1.time) < 0.01f || Math.Abs(op1.time - op1.clip.length) < 0.1f)
                if (op1.time >= op1.clip.length - 0.08f || !op1.isPlaying)
                {
                    status = 2;
//                    loop.time = 75;
                    loop.Play();
                }
        }
        else if (status == 2)
        {
        }

        if (isFadingOut)
        {
            this.curTime -= Time.deltaTime;
            if (this.curTime <= 0)
            {
                playing.Stop();
                isFadingOut = false;
            }
            else
            {
                playing.volume = maxVolume * this.curTime / this.duration;
            }
        }
    }

    public void Stop(float duration)
    {
        status = 3;
        if (loop.isPlaying)
        {
            playing = loop;
            maxVolume = loop.volume;
            this.duration = duration;
            this.curTime = duration;
            this.isFadingOut = true;
        }
        else if (op1.isPlaying)
        {
            playing = op1;
            maxVolume = op1.volume;
            this.duration = duration;
            this.curTime = duration;
            this.isFadingOut = true;
        }
    }
}