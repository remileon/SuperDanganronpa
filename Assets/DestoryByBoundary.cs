using UnityEngine;
using System.Collections;

public class DestoryByBoundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.tag + " exit");
        if (other.gameObject.tag.Equals("bullet") || other.gameObject.tag.Equals("enemy_bullet"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag.Equals("bw"))
        {

        }
    }
}
