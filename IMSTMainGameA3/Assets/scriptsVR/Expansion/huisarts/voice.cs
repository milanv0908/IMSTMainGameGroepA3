
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voice : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip start;
    public AudioClip scan;
    public AudioClip diagnosistext;  // Corrected variable name
    public AudioClip diagnosistext2;
    public AudioClip headsetof;
    public bool buttonpress = false;
    public bool diagnosis = false;   // Corrected variable name
    public bool diagnosis2 = false;
    public bool headsetoff = false;

    void Start() {
        audiosource = GetComponent<AudioSource>();
        audiosource.PlayOneShot(start);
    }

    void Update() {
        if (buttonpress) {  // Use 'if (buttonpress)' instead of 'if (buttonpress == true)'
            audiosource.PlayOneShot(scan);
            buttonpress = false;
        }

        if (diagnosis) {   // Use 'if (diagnosis)' instead of 'if (diagnosis == true)'
            audiosource.PlayOneShot(diagnosistext);
            diagnosis = false;
        }

        if (diagnosis2) {
            audiosource.PlayOneShot(diagnosistext2);
            diagnosis2 = false;
        }



        if(headsetoff) {
            audiosource.PlayOneShot(headsetof);
            headsetoff = false;
        }
    }
}
