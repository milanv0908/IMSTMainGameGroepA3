using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class hoofddeur : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;
    public deurklink deurklink;
    public bool isopen;
    private bool isCoroutineRunning = false;
    public TextMeshProUGUI text;
    // public speler player;
    public spelertelefoon spelerbool;

    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        isopen = false;
        text.enabled = false;
    }

    void Update()
    {
        if (deurklink.isActivated == true && spelerbool.DeurOpen == false)
        {
            if (!isCoroutineRunning) // Check if the coroutine is already running
            {
                text.enabled = true;
                text.text = "You can't leave the apartment yet!";
                Debug.Log("hoi");
                StartCoroutine(tekst());
            }
        }

        if (isopen == false && deurklink.isActivated == true && !isCoroutineRunning && spelerbool.DeurOpen == true)
        {
            StartCoroutine(isopen1());
        }

        if (isopen == true && deurklink.isActivated == true && !isCoroutineRunning && spelerbool.DeurOpen == true)
        {
            StartCoroutine(isdicht());
        }
    }

    IEnumerator tekst()
    {
        isCoroutineRunning = true; // Set the flag to indicate the coroutine is running
        yield return new WaitForSeconds(3);
        text.enabled = false;
        isCoroutineRunning = false; // Reset the flag when the coroutine is done
    }

    IEnumerator isopen1()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("open");
        isopen = true;
        Debug.Log("open");
        isCoroutineRunning = false;
    }

    IEnumerator isdicht()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("sluit");
        isopen = false;
        Debug.Log("Sluiten");
        isCoroutineRunning = false;
    }
}

