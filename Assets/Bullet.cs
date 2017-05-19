using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Transform trans;
    
    [SerializeField] private float speed = 100000f;

    void Awake()
    {
        trans = GetComponent<Transform>();
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        float distance = (Time.deltaTime * speed);
        Debug.Log(Time.deltaTime);
        Debug.Log(distance);
        float deltaX = -Mathf.Sin(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        float deltaY = Mathf.Cos(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        trans.position += new Vector3(deltaX, deltaY, 0); 
	}
}
