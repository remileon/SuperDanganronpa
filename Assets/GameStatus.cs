using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using common_scripts;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public bool isDebugging = true;
    public static GameStatus Instance { get; private set; }

    public int bwLife = 3;
    public int enemyProgress = 10;
    public BwBuff[] bwBuffs = {};
    public int failCount = 0;

    // temps
    public string scenario = "fail";
//    public float bwDelay = 4.15f;
    // todo: deving
    public float bwDelay = 0.15f;
    public bool isShuttingDown = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        isShuttingDown = true;
    }
}
