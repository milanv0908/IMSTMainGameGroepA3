using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerTutorial : MonoBehaviour
{
    public Animator animator;
    private bool hasRunPopIn = false;
    private bool hasShowed;
    public Image image;
    public static bool gameIsPaused;

    void Start()
    {
        hasShowed = false;
        image.enabled = false;
    }

    void OnTriggerEnter()
    {
        if (!hasShowed)
        {
            image.enabled = true;
            hasShowed = true;
            animator.SetTrigger("PopInTrigger");
            // Debug.Log("werken!!!!!!");
            StartCoroutine(RunPopInThenPause());
            // pause();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameIsPaused == true)
        {
            animator.SetTrigger("PopOut");
            // image.enabled = false;
            resume();
        }
    }

    public void resume()
    {
        // image.enabled = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void pause()
    {
        image.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private IEnumerator RunPopInThenPause()
    {
        if (!hasRunPopIn)
        {
            animator.SetTrigger("PopInTrigger");
            hasRunPopIn = true;
            yield return new WaitForSeconds(0.8f);
            gameIsPaused = true;
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
