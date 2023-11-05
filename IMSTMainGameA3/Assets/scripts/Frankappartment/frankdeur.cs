using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frankdeur : MonoBehaviour
{
    public williambellen deur;
    Animator animator;
    void Start()
    {
      animator = GetComponent<Animator>();  
    }

    void Update()
    {
    if (deur.dooropen == true) {
        animator.SetTrigger("open");
    }
        
    }
}
