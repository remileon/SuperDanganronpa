using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    private Transform trans;

    [SerializeField] private float speed = 5.0f;
    private float life = 10f;

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
    }
}
