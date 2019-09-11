using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Bw : MonoBehaviour
{
    private static readonly int Invincible = Animator.StringToHash("invincible");
    private Animator animator;
    public Boundary boundary;
    public Director director;

    private double protectTime;

    private Transform trans;
    public UiLife uiLife;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    private void Start()
    {
        uiLife = GameObject.Find("LifeGroup").GetComponent<UiLife>();
        uiLife.Init(GameStatus.Instance.bwLife);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        if (protectTime > 0)
        {
            protectTime = Math.Max(0, protectTime - Time.deltaTime);
            if (protectTime <= 0) animator.SetBool(Invincible, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckCollide(other)) DoCollide();
    }

    private void Move()
    {
        float rotateSpeed = 120;
        var moveSpeed = 5.0f;
        var keyConfig = KeyConfig.Instance;

        if (Input.GetKey(keyConfig.turnLeft)) trans.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        if (Input.GetKey(keyConfig.turnRight)) trans.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));

        var distance = Time.deltaTime * moveSpeed;
        var deltaX = -Mathf.Sin(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        var deltaY = Mathf.Cos(trans.rotation.eulerAngles.z / 360f * 2 * Mathf.PI) * distance;
        if (Input.GetKey(keyConfig.moveUp)) trans.position += new Vector3(deltaX, deltaY, 0);
        if (Input.GetKey(keyConfig.moveDown)) trans.position -= new Vector3(deltaX, deltaY, 0);
        if (Input.GetKey(keyConfig.moveLeft)) trans.position += new Vector3(-deltaY, deltaX, 0);
        if (Input.GetKey(keyConfig.moveRight)) trans.position += new Vector3(deltaY, -deltaX, 0);

        var moveX = Input.GetAxis("JSMoveX");
        if (moveX > 0.5)
            trans.position += new Vector3(distance, 0, 0);
        else if (moveX < -0.5) trans.position += new Vector3(-distance, 0, 0);

        var moveY = Input.GetAxis("JSMoveY");
//        Debug.Log("move y: " + moveY);
        if (moveY > 0.5)
            trans.position += new Vector3(0, -distance, 0);
        else if (moveY < -0.5) trans.position += new Vector3(0, distance, 0);

        var directionX = Input.GetAxis("JSDirectionX");
        var directionY = Input.GetAxis("JSDirectionY");
        if (Math.Abs(directionX) > 0.5 || Math.Abs(directionY) > 0.5) {
            Debug.Log("direction x: " + directionX + ", direction y: " + directionY);
            trans.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), new Vector3(directionX, -directionY, 0));
        }


        trans.position = new Vector3(
            Mathf.Clamp(trans.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(trans.position.y, boundary.yMin, boundary.yMax),
            0
        );
    }

    private bool CheckCollide(Collider other)
    {
        var collide = false;
        string[] tags = {"enemy_bullet_red", "enemy_bullet_orange"};
        foreach (var tag in tags)
            if (tag.Equals(other.gameObject.tag))
                collide = true;
        return collide;
    }

    public void DoCollide()
    {
        if (protectTime > 0) return;

        protectTime = 1f;
        animator.SetBool(Invincible, true);
        var gameStatus = GameStatus.Instance;
        --gameStatus.bwLife;
        uiLife.LoseLife();
        if (gameStatus.bwLife <= 0)
        {
            // todo : 死亡特效
            ++gameStatus.failCount;
            gameStatus.scenario = "fail";
            director.Cut();
            SceneManager.LoadScene("avg/avg", LoadSceneMode.Single);
        }
    }
}