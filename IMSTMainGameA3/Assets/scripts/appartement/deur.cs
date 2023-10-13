using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deur : MonoBehaviour
{
    Animator animator;
    public bool isopen;
    void Start()
    {
        animator = GetComponent<Animator>();
        isopen = false;
    }
    public void open()
    {
        if (isopen == false)
        {
            animator.SetTrigger("open");
            isopen = true;
        } else {
            animator.SetTrigger("sluit");
            isopen = false;
        }
    }
}
