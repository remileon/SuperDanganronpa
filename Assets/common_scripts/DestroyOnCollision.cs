using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    private Transform trans;
    public GameObject ExplodeEffect;
    public string[] tags;
    public int life = 1;
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (checkCollide(other))
        {
            doCollide();
        }
    }

    bool checkCollide(Collider other)
    {
        bool collide = false;
        foreach (string tag in tags)
        {
            if (tag.Equals(other.gameObject.tag))
            {
                collide = true;   
            }
        }
        return collide;
    }

    void doCollide()
    {
        --life;
        if (life <= 0)
        {
            if (ExplodeEffect != null)
            {
                GameObject effect = Instantiate(ExplodeEffect, trans.position + new Vector3(), trans.rotation) as GameObject;
            }
            Destroy(gameObject);
        }
    }
}
