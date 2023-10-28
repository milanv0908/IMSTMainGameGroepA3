using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class steptutorial : MonoBehaviour
{
    private bool hasShowed;
    public Image image;
    public static bool gameIsPaused;

    void Start()
    {
        hasShowed = false;
        image.enabled = false;
    }

    void OnTriggerEnter() {
        if(hasShowed == false) {
            image.enabled = true;
            pause();
            hasShowed = true;

        }

    }
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E)) {
            image.enabled = false;
            resume();

     }  
    }

            public void resume () {
        image.enabled = false;
Time.timeScale = 1f;
gameIsPaused = false;
    }

    void pause () {
        image.enabled = true;
Time.timeScale = 0f;
gameIsPaused = true;
    }
}
