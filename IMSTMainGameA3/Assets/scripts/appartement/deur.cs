

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deur : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;
    public deurklink deurklink;
    public bool isopen;
    private bool isCoroutineRunning = false;
    public AudioClip deuropen;
    public AudioClip deurdicht;

    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        isopen = false;
    }

    public void Update()
    {
        if (isopen == false && deurklink.isActivated == true && !isCoroutineRunning)
        {
            StartCoroutine(isopen1());
        }

        if (isopen == true && deurklink.isActivated == true && !isCoroutineRunning)
        {
            StartCoroutine(isdicht());
        }
    }

    IEnumerator isopen1()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("open");
        isopen = true;
        Debug.Log("open");
        isCoroutineRunning = false;
        audiosource.PlayOneShot(deuropen);
    }

    IEnumerator isdicht()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("sluit");
        isopen = false;
        Debug.Log("Sluiten");
        isCoroutineRunning = false;
        audiosource.PlayOneShot(deurdicht);
    }
}