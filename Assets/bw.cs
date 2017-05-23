﻿using UnityEngine;
using System.Collections;

public class bw : MonoBehaviour {

    public Transform trans;
    public GameObject bullet;
    [SerializeField] private float maxInterval = 0.5f;
    private float interval;

    void Awake()
    {
        trans = GetComponent<Transform>();
        interval = maxInterval;
    }

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        interval -= Time.deltaTime;
        while (interval <= 0)
        {
            GameObject newBullet = Instantiate(bullet, trans.position + new Vector3(), trans.rotation) as GameObject;
            interval += maxInterval;
        }

        float rotateSpeed = 120;
        if (Input.GetKey(KeyCode.Q)) { 
            trans.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.E))
        {
            trans.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));
        }
        //trans.position -= new Vector3(Time.deltaTime, 0, 0);

        float speed = 5;
        float distance = (Time.deltaTime * speed);
        float deltaX = -Mathf.Sin(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        float deltaY = Mathf.Cos(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        if (Input.GetKey(KeyCode.W))
        {
            trans.position += new Vector3(deltaX, deltaY, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            trans.position -= new Vector3(deltaX, deltaY, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            trans.position += new Vector3(-deltaY, deltaX, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            trans.position += new Vector3(deltaY, -deltaX, 0);
        }
    }
}
