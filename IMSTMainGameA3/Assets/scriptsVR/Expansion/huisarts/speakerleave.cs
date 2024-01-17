using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakerleave : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip announcement;
    public TV tv;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (tv.leave == true) {
            audiosource.PlayOneShot(announcement);
        }
    }
}
