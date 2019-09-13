using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private Transform trans;
    
    public float speed = 5.0f;
    private float life = 1.3f;

    void Awake()
    {
        trans = GetComponent<Transform>();
    }
	// Use this for initialization
	void Start () {
        float deltaX = -Mathf.Sin(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI);
        float deltaY = Mathf.Cos(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI);
        float deltaZ = 0;
        
//        GetComponent<Rigidbody>().velocity = new Vector3(deltaX, deltaY, deltaZ) * speed;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = (Time.deltaTime * speed);
        float deltaX = -Mathf.Sin(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        float deltaY = Mathf.Cos(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        trans.position += new Vector3(deltaX, deltaY, 0);

        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(gameObject);
        }

        if (trans.position.y >= 4.29f)
        {
	        Destroy(gameObject);
        }
	}
}
