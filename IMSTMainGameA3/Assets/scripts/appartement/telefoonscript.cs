using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telefoonscript : MonoBehaviour
{
    AudioSource audiosource;
    public koffiemok koffie;

    void Start()
    {
    audiosource = GetComponent<AudioSource>();
        audiosource.enabled = false;
    }

    void Update()
    {
        if (koffie.telefoonfrank == true) {
            audiosource.enabled = true;
            } else {
            audiosource.enabled = false;
            }
    }
}
