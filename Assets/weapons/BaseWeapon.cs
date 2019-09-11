using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private AudioSource audioSource;
    private Transform baseTrans;
    public GameObject bullet;
    public float? bulletSpeed;
    public float delay;
    public float initRotate;
    private float nextShoot;
    public float rotateSpeed;

    public float shootInterval = 0.2f;
    private bool hasAudio;


    public void Awake()
    {
        baseTrans = GetComponent<Transform>();

        audioSource = GetComponent<AudioSource>();
        baseTrans.Rotate(new Vector3(0, 0, initRotate));
    }

    public void Start()
    {
        hasAudio = audioSource != null;
        nextShoot = delay;
    }

    private void Update()
    {
        var remainingTime = Time.deltaTime;
        while (remainingTime > 0.00001)
        {
            float consumedTime;
            if (nextShoot < remainingTime)
                consumedTime = nextShoot;
            else
                consumedTime = remainingTime;
            // rotate
            baseTrans.Rotate(new Vector3(0, 0, rotateSpeed * consumedTime));
            // prepare shoot
            nextShoot -= consumedTime;
            // shoot
            if (nextShoot < 0.00001)
            {
                Shoot();
                if (hasAudio) audioSource.Play();
                nextShoot = shootInterval;
            }

            // consume time
            remainingTime -= consumedTime;
        }
    }

    public abstract void Shoot();

    protected void BaseModify(GameObject newBullet)
    {
        if (bulletSpeed != null)
        {
            EnemyBullet enemyBullet = newBullet.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
            {
                enemyBullet.speed = bulletSpeed.Value;
            }
            Bullet bullet = newBullet.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.speed = bulletSpeed.Value;
            }
        }
    }

    public void ModifyAttributes(Dictionary<string, object> dict)
    {
        if (dict.ContainsKey("shootInterval")) shootInterval = (float) dict["shootInterval"];
        if (dict.ContainsKey("rotateSpeed")) rotateSpeed = (float) dict["rotateSpeed"];
        if (dict.ContainsKey("bullet")) bullet = dict["bullet"] as GameObject;

        if (dict.ContainsKey("delay")) delay = (float) dict["delay"];
        if (dict.ContainsKey("bulletSpeed")) bulletSpeed = (float) dict["bulletSpeed"];
    }
}