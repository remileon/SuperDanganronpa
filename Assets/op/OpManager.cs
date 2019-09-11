using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("NextScene", 9);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NextScene()
    {
	    GameStatus.Instance.bwDelay = 4.15f;
        SceneManager.LoadScene("battle", LoadSceneMode.Single);
    }
}
