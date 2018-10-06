using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private Transform baseTrans;
    private AudioSource audio;

    public float shootInterval = 0.2f;
    public GameObject bullet;
    public float rotateSpeed = 0;
    public float initRotate = 0;
    private float nextShoot = 0;

    public void Awake()
    {
        baseTrans = GetComponent<Transform>();

        audio = GetComponent<AudioSource>();
        baseTrans.Rotate(new Vector3(0, 0, initRotate));
        nextShoot = shootInterval;
    }

    void Update()
    {
        float remainingTime = Time.deltaTime;
        while (remainingTime > 0.00001)
        {
            float consumedTime;
            if (nextShoot < remainingTime)
            {
                consumedTime = nextShoot;
            }
            else
            {
                consumedTime = remainingTime;
            }
            // rotate
            baseTrans.Rotate(new Vector3(0, 0, rotateSpeed * consumedTime));
            // prepare shoot
            nextShoot -= consumedTime;
            // shoot
            if (nextShoot < 0.00001)
            {
                Shoot();
                if (audio != null)
                {
                    audio.Play();
                }
                nextShoot = shootInterval;
            }
            // consume time
            remainingTime -= consumedTime;
        }
    }

    public abstract void Shoot();

    public void ModifyAttributes(Dictionary<string, object> dict)
    {
        if (dict.ContainsKey("shootInterval"))
        {
            shootInterval = (float)dict["shootInterval"];
        }
        if (dict.ContainsKey("rotateSpeed"))
        {
            rotateSpeed = (float)dict["rotateSpeed"];
        }
        if (dict.ContainsKey("bullet"))
        {
            bullet = dict["bullet"] as GameObject;
        }
    }
}

