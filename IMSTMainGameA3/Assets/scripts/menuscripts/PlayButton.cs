using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

   public void PlayGame()
{
    Time.timeScale = 1f; // Stel de tijd schaal in op 1
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
