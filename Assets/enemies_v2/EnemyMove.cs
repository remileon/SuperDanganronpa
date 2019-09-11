using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Vector3 activePos = new Vector3(0, 0, 0);
    public float activeTime = 5f;
    private readonly List<float> cubicLengths = new List<float>();


    // [[p1, p2, p3], [p1, p2, p3], [p1, p2, p3] ...]
    private List<Vector3[]> cubicPaths = new List<Vector3[]>();


    private float currentTime;
    public Vector3 exitPos = new Vector3(0, 5, 0);
    public float exitTime = 0.2f;
    public Vector3 initPos = new Vector3(0, 4.62f, 0);


    public float initTime = 0.2f;
    private float totalCubicLength;
    private Transform trans;

    public void SetCubicPath(List<Vector3[]> cubicPaths)
    {
        this.cubicPaths = cubicPaths;
        for (var i = 0; i < cubicPaths.Count; i++)
        {
            var length = BezierUtils.CubicLength(
                i == 0 ? new Vector3(0, 0, 0) : cubicPaths[i - 1][2],
                cubicPaths[i][0],
                cubicPaths[i][1],
                cubicPaths[i][2]
            );
            cubicLengths.Add(length);
            totalCubicLength += length;
        }
    }

    private void Awake()
    {
        trans = GetComponent<Transform>();
        trans.position = initPos;
    }

    // Update is called once per frame
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime <= initTime)
        {
            trans.position = Vector3.Lerp(initPos, activePos, currentTime / initTime);
        }
        else if (currentTime <= initTime + activeTime)
        {
            if (cubicPaths.Count == 0)
            {
                trans.position = activePos;
            }
            else
            {
                var t = (currentTime - initTime) / activeTime;
                var length = totalCubicLength * t;
                var curLength = 0f;
                int curPathIdx;
                for (curPathIdx = 0; curPathIdx < cubicPaths.Count; ++curPathIdx)
                {
                    if (curLength + cubicLengths[curPathIdx] >= length) break;

                    curLength += cubicLengths[curPathIdx];
                }

                var relativePosition = BezierUtils.Cubic(
                    (length - curLength) / cubicLengths[curPathIdx],
                    curPathIdx > 0 ? cubicPaths[curPathIdx - 1][2] : new Vector3(0, 0, 0),
                    cubicPaths[curPathIdx][0],
                    cubicPaths[curPathIdx][1],
                    cubicPaths[curPathIdx][2]
                );

                trans.position = activePos + relativePosition;
            }
        }
        else if (currentTime <= initTime + activeTime + exitTime)
        {
            trans.position = Vector3.Lerp(activePos + (cubicLengths.Count <= 0 ? new Vector3(0, 0,0 ) : cubicPaths[cubicPaths.Count - 1][2]), exitPos,
                (currentTime - initTime - activeTime) / exitTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}