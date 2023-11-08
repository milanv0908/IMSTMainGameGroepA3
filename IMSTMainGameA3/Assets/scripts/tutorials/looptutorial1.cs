using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class looptutorial1 : MonoBehaviour
{
    public static bool gameIsPaused;
    public Image image;
    public Animator animator;

    private bool hasRunPopIn = false;

    void Start()
    {
        image.enabled = true;
        // StartCoroutine(RunPopInThenPause());
        Debug.Log("pause");
    }

public void startCoroutineMenuShits(){
    hasRunPopIn = false;
    StartCoroutine(RunPopInThenPause());
}
    void Update()
    {
        if (gameIsPaused == true && Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("PopOut");
            resume();
            Debug.Log("resume");
        }
    }

    private void resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
         hasRunPopIn = true;
        if(Input.GetKeyDown(KeyCode.E)){
            resume();
        }
    }

    private IEnumerator RunPopInThenPause()
    {
        if (!hasRunPopIn)
        {
            animator.SetTrigger("PopInTrigger");
            hasRunPopIn = true;
            yield return new WaitForSeconds(0.8f);
            gameIsPaused = true;
            Debug.Log("hiii");
        }
        else
        {
            while (gameIsPaused)
            {
                yield return null; // Wacht totdat de game niet meer is gepauzeerd.
            }
        }

        pause();
    }
}
