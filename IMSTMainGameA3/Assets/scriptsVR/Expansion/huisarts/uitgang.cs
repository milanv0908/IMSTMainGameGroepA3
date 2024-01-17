using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uitgang : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;
    public AudioClip deuropen;
    public TV tv;
    void Start()
    {
    audiosource = GetComponent<AudioSource>();
    animator = GetComponent<Animator>();
        tv.leave = false;
    }


    void Update()
    {
        if (tv.leave == true) {
            audiosource.PlayOneShot(deuropen);
            animator.SetTrigger("open");
            tv.leave = false;
            Debug.Log("hoppa");
        }
    }
}
