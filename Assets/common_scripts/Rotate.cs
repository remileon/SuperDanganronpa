using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Transform baseTrans;

    public float rotateSpeed = 0;
    public float initRotate = 0;

    public void Awake()
    {
        baseTrans = GetComponent<Transform>();
        baseTrans.Rotate(new Vector3(0, 0, initRotate));
    }

    void Update()
    {
        float remainingTime = Time.deltaTime;
        baseTrans.Rotate(new Vector3(0, 0, rotateSpeed * remainingTime));
    }
}
