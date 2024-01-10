using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williamgaatlos : MonoBehaviour
{
    public frank Frank;
    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  // Fix the typo here
        audioSource.enabled = false;
    }

    void Update()
    {
        if (Frank.animation == true)
            animator.SetTrigger("start");

        if (Frank.beginend == true)
        {
            audioSource.enabled = true;
        }
    }
}

