using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoest : MonoBehaviour
{

AudioSource audiosource;
public streetevent Streetevent;
public AudioClip scheur;
    void Start()
    {
    audiosource = GetComponent<AudioSource>();
        audiosource.enabled = false;  
    }

    void Update()
    {
       if (Streetevent.hasinteracted == true) {
            audiosource.enabled = true;
            audiosource.PlayOneShot(scheur);
       } 
    }
}
