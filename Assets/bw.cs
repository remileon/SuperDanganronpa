using UnityEngine;
using System.Collections;

public class bw : MonoBehaviour {

    public Transform trans;
    public GameObject bullet;
    public float maxInterval = 0.5f;
    public float interval;

    void Awake()
    {
        trans = GetComponent<Transform>();
        bullet = GameObject.Find("bullet");
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
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = trans.position + new Vector3(0, 0, 0);
            newBullet.transform.rotation = trans.rotation;
            interval += maxInterval;
        }
        //trans.position -= new Vector3(Time.deltaTime, 0, 0);
    }
}
