using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class bw : MonoBehaviour
{

    private Transform trans;
    public Boundary boundary;
    public UiLife uiLife;

    private double protectTime = 0f;
    
    void Awake()
    {
        trans = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start()
    {
        uiLife = GameObject.Find("LifeGroup").GetComponent<UiLife>();
        uiLife.Init(GameStatus.Instance.bwLife);
    }
	
    // Update is called once per frame
    void Update()
    {
        this.Move();
        this.protectTime = Math.Max(0, this.protectTime - Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (CheckCollide(other))
        {
            DoCollide();
        }
    }

    void Move()
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

    bool CheckCollide(Collider other)
    {
        bool collide = false;
        string[] tags = {"enemy_bullet_red", "enemy_bullet_orange"};
        foreach (string tag in tags)
        {
            if (tag.Equals(other.gameObject.tag))
            {
                collide = true;   
            }
        }
        return collide;
    }

    void DoCollide()
    {
        if (protectTime > 0)
        {
            return;
        }

        protectTime = 1f;
        var gameStatus = GameStatus.Instance;
        --gameStatus.bwLife;
        uiLife.LoseLife();
        if (gameStatus.bwLife <= 0)
        {
            // todo : 死亡特效
            ++gameStatus.failCount;
            gameStatus.scenario = "fail";
            SceneManager.LoadScene("avg/avg", LoadSceneMode.Single);
        }
    }
}
