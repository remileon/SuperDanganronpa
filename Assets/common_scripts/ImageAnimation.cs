using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnimation : MonoBehaviour {

    public Sprite[] frames;
    public int framesPerSecond = 10;
    private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        int index = ((int) (Time.time * framesPerSecond)) % frames.Length;
        renderer.sprite = frames[index];
	}
}
