using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }
    
    public Animator animator;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    private string nextScene;
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
    }
    
    public void FadeToAvg()
    {
        nextScene = "avg/avg";
        
        animator.SetTrigger(FadeOut);
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(nextScene);
        animator.SetTrigger(FadeIn);
    }
}
