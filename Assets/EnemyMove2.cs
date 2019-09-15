using System;
using UnityEngine;

public class EnemyMove2 : MonoBehaviour
{
    public float endTime = 8f;
    public float gravity;
    public Vector3 initSpeed;

    public float k;
    public float returnTime = 6f;
    public float slowRatios = 0.4f;

    public float slowTime = 1.5f;

    private Vector3 speed;

    private float time;
    private Transform transform;

    // Start is called before the first frame update
    private void Start()
    {
        speed = initSpeed;
        transform = gameObject.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        var t = Time.deltaTime;
        time += t;
        if (time < returnTime)
        {
            if (time > slowTime)
            {
                if (time > slowTime + 0.5f)
                    t = t * slowRatios;
                else
                    t = t * (((time - slowTime) / 0.5f) * (1 - slowRatios) + slowRatios);
            }
        }
        else
        {
            t = t * 3;
        }

        if (time > endTime) Destroy(gameObject);
        transform.position = transform.position + speed * t;
        var a = new Vector3(-Math.Sign(speed.x) * k * speed.x * speed.x,
            gravity - Math.Sign(speed.y) * k * speed.y * speed.y, -Math.Sign(speed.z) * k * speed.z * speed.z);
        speed += a * t;
    }
}