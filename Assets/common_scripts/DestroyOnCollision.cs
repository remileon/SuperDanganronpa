using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    // Use this for initialization

    private float age;
    public GameObject ExplodeEffect;
    public int life = 1;
    public float startAge = 0;
    public string[] tags;

    private Transform trans;

    private void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        age += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkCollide(other)) doCollide();
    }

    private bool checkCollide(Collider other)
    {
        if (age < startAge) return false;
        var collide = false;
        foreach (var tag in tags)
            if (tag.Equals(other.gameObject.tag))
                collide = true;
        return collide;
    }

    private void doCollide()
    {
        --life;
        if (life <= 0)
        {
            if (ExplodeEffect != null)
            {
                var effect = Instantiate(ExplodeEffect, trans.position + new Vector3(), trans.rotation);
            }

            Destroy(gameObject);
        }
    }
}