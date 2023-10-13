using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koffiezetapparaat : MonoBehaviour
{
    Animator animator;
    void Start()
    {
    animator = GetComponent<Animator>(); 
    }

    public void koffieZetten()
    {
        animator.SetTrigger("koffie");
    }
}
