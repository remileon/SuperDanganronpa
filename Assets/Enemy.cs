using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public Transform trans;
    public GameObject bullet;
    [SerializeField]
    private float maxInterval = 0.5f;
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

    }
}
