using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImg : MonoBehaviour
{
    
    public Sprite[] images;
    public Image imageScript;

    public bool isShowing { get; private set; } = false;
    private float showingTimeout = 0;
    private Action callback;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing)
        {
            showingTimeout -= Time.deltaTime;
            if (showingTimeout <= 0)
            {
                isShowing = false;
                callback.Invoke();
            }
        }
    }
    
    public void Show(int idx, float duration, Action callback)
    {
        imageScript.sprite = images[idx];
//        imageScript.gameObject.SetActive(true);
        imageScript.CrossFadeAlpha(1, duration, false);
        showingTimeout = duration;
        this.callback = callback;
        this.isShowing = true;
    }

    public void Hide()
    {
        imageScript.CrossFadeAlpha(0, 0, false);
//        imageScript.gameObject.SetActive(false);
    }
}
