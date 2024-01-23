using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioAwake : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip introAudio;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(introAudio);
    }
}
