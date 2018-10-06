using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {
    private Transform trans;
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;

	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        Instantiate(particleSystem1, trans.position + new Vector3(), trans.rotation).Play();
        Instantiate(particleSystem2, trans.position + new Vector3(), trans.rotation).Play();
        Destroy(gameObject);
	}
	
	// Update is called once per frame
    void Update () {
//        particleSystem1.Play();
//        particleSystem2.Play();
	}
}
