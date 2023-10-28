using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class looptutorial : MonoBehaviour
{
 public static bool gameIsPaused;
    public Image image;
    void Start()
    {
        image.enabled = true;
        pause();
        Debug.Log("pause");
    }

    void Update()
    {
     if (Input.GetKeyDown(KeyCode.E)) {
            image.enabled = false;
            resume();
            Debug.Log("resume");
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
