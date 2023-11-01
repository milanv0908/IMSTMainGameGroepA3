using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platenspeler : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip dikkebeat;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    private float activationDistance = 5.0f;
    public Transform player; // Spelerreferentie

    public GameObject muziekparticles;

    private bool isPlaying = false; // Flag to track if audio is already playing

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        UIUpdate.SetActive(false);
        muziekparticles.SetActive(false);
    }

    public void beat()
    {
        if (!isPlaying)
        {
            audiosource.time = 0f;
            audiosource.PlayOneShot(dikkebeat);
            isPlaying = true; // Set the flag to indicate audio is playing
            muziekparticles.SetActive(true);
        }
    }

    // Add this method to reset the flag when the audio is finished
    void Update()
    {
        if (!audiosource.isPlaying)
        {
            isPlaying = false;
            muziekparticles.SetActive(false);
        }
    }
}
