using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public Transform trans;

    [SerializeField]
    private float speed = 10f;


    // Use this for initialization
    void Start () {
        float deltaX = -Mathf.Sin(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI);
        float deltaY = Mathf.Cos(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI);
        float deltaZ = 0;

        GetComponent<Rigidbody>().velocity = new Vector3(deltaX, deltaY, deltaZ) * speed;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
