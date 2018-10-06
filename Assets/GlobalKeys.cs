using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalKeys : MonoBehaviour
{

    public KeyCode quit = KeyCode.Escape;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(quit))
        {
            Application.Quit();
        }
    }
}
