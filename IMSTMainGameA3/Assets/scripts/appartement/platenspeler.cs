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
    public bool musicon = false;

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
            musicon = true;
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

                if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && musicon == false)
        {
            UIUpdate.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }
    }
}
