using UnityEngine;
using System.Collections;

public class bw : MonoBehaviour
{

	public Transform trans;
	public GameObject bullet;
	[SerializeField] private float maxInterval = 0.5f;
	private float interval;

	void Awake()
	{
		trans = GetComponent<Transform>();
		interval = maxInterval;
	}

	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		interval -= Time.deltaTime;
		while (interval <= 0)
		{
			GameObject newBullet = Instantiate(bullet, trans.position + new Vector3(), trans.rotation) as GameObject;
			interval += maxInterval;
		}
		this.move();
	}

	void move()
	{

		float rotateSpeed = 120;
		float moveSpeed = 5;
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
	}
}
