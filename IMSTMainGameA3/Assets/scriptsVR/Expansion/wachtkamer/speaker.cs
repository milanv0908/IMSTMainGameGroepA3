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
    }

    
    void Update()
    {
        if(Voice.newscene == true) {
            audiosource.PlayOneShot(waitingdone);
            Voice.newscene = false;
        }
    }
}
