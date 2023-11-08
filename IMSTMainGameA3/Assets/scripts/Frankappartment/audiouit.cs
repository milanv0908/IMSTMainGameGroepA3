using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiouit : MonoBehaviour
{
    AudioSource audiosource;
public frank Frank;
    void Start()
    {
    audiosource = GetComponent<AudioSource>();  
    }

    

    void Update()
    {
        if (Frank.beginend == true) {
            audiosource.enabled = false;
        }
    }
}
