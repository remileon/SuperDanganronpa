using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgStarGenerator : MonoBehaviour
{

    public float interval = 0.2f;
    public GameObject starEffect;

    private float next = 0f;

    // Use this for initialization
    void Start ()
    {
        next = interval;
	}
	
	// Update is called once per frame
	void Update ()
    {

        float remainingTime = Time.deltaTime;

        while (remainingTime > 0.00001)
        {

            float consumedTime;
            if (next < remainingTime)
            {
                consumedTime = next;
            }
            else
            {
                consumedTime = remainingTime;
            }

            next -= consumedTime;
            if (next < 0.00001) { 
                float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

                Instantiate(starEffect, spawnPosition, Quaternion.identity);
                // Instantiate(starEffect);

                next = interval;
            }

            remainingTime -= consumedTime;
        }
    }
}
