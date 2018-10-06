using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class bw : MonoBehaviour
{

    private Transform trans;
    public Boundary boundary;

    void Awake()
    {
        trans = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start()
    {
        
    }
	
    // Update is called once per frame
    void Update()
    {
        this.move();
    }

    void move()
    {

        float rotateSpeed = 120;
        float moveSpeed = 5.0f;
        KeyConfig keyConfig = KeyConfig.Instance;

        if (Input.GetKey(keyConfig.turnLeft))
        { 
            trans.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        }
        if (Input.GetKey(keyConfig.turnRight))
        {
            trans.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));
        }

        float distance = (Time.deltaTime * moveSpeed);
        float deltaX = -Mathf.Sin(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        float deltaY = Mathf.Cos(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        if (Input.GetKey(keyConfig.moveUp))
        {
            trans.position += new Vector3(deltaX, deltaY, 0);
        }
        if (Input.GetKey(keyConfig.moveDown))
        {
            trans.position -= new Vector3(deltaX, deltaY, 0);
        }
        if (Input.GetKey(keyConfig.moveLeft))
        {
            trans.position += new Vector3(-deltaY, deltaX, 0);
        }
        if (Input.GetKey(keyConfig.moveRight))
        {
            trans.position += new Vector3(deltaY, -deltaX, 0);
        }
        trans.position = new Vector3(
            Mathf.Clamp(trans.position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp(trans.position.y, boundary.yMin, boundary.yMax),
            0
        );
    }
}
