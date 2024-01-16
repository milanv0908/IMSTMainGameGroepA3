using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speaker : MonoBehaviour
{
    AudioSource audiosource;

    public nieuwescene Voice;
    public AudioClip waitingdone;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        BoostVolume(); // Call the BoostVolume function to boost the sound volume
    }

    void Update()
    {
        if (Voice.newscene)
        {
            audiosource.PlayOneShot(waitingdone);
            Voice.newscene = false;
        }
    }

    void BoostVolume()
    {
        // Add the following lines to boost the volume of the AudioSource
        if (audiosource != null)
        {
            float boostFactor = 3.5f; // You can adjust this according to your preference
            audiosource.volume *= boostFactor;
        }
        else
        {
            Debug.LogError("AudioSource is not assigned! Please assign it in the editor.");
        }
    }
}
