using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{

    public static BgmController Instance { get; private set; }
    public AudioSource op1;

    public AudioSource op2;

    public AudioSource loop;

    public int status = 1;
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
        op1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == 1)
        {
            if (!op1.isPlaying)
            {
                status = 2;
                op2.Play();
            }
        }
        else if (status == 2)
        {
            if (!op2.isPlaying)
            {
                status = 3;
                loop.Play();
            }
        } else if (status == 3)
        {
        }
    }
}
