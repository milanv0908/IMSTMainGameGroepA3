using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williamgaatlos : MonoBehaviour
{
    public frank Frank;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Frank.animation == true)
       animator.SetTrigger("start"); 
    }
}
