﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyConfig : MonoBehaviour {

	public static KeyConfig Instance { get; private set; }
	public KeyCode moveUp = KeyCode.W;
	public KeyCode moveDown = KeyCode.S;
	public KeyCode moveLeft = KeyCode.A;
	public KeyCode moveRight = KeyCode.D;
	public KeyCode turnLeft = KeyCode.Q;
	public KeyCode turnRight = KeyCode.E;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
