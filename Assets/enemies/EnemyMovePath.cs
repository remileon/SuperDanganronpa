using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovePath : MonoBehaviour
{

    public MoveToPoint[] points;
    public bool destoryOnEnd = false;

    private Transform trans;
    private int curIndex = 0;
    private float curTime = 0;
    private Vector3 initPosition;

    void Awake()
    {
        trans = GetComponent<Transform>();
        initPosition = trans.position;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        while (curIndex < points.Length && curTime > points[curIndex].time)
        {
            curTime -= points[curIndex].time;
            ++curIndex;
        }
        if (curIndex < points.Length)
        {
            UpdatePosition();
        }
        else
        {
            if (destoryOnEnd)
            {
                Destroy(gameObject);
            }
        }
    }

    void UpdatePosition()
    {
        Vector3 from = curIndex == 0 ? initPosition : points[curIndex - 1].position;
        Vector3 to = points[curIndex].position;
        float duration = points[curIndex].time;

        float rate = curTime / duration;
        Vector3 position = from + (to - from) * rate;

        trans.position = position;
    }
}
