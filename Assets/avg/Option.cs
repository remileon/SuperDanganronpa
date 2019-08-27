using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private Animator animator;
    private static readonly int hovered = Animator.StringToHash("hovered");

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool(hovered, true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool(hovered, false);
        }
    }
}
