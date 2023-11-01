using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
public CursorState cursorState;
public static bool gameIsPaused;
public GameObject pauseMenuUI;

 public Camerabob Camerabob;

public PlayerMove playerMovement;

 public speler player;
void Start() {
    resume();
}

    void Update()
    {
       if(cursorState.cursorState == true) {
        Cursor.lockState = CursorLockMode.None;
       }

       if (Input.GetKeyDown(KeyCode.Escape)) {
        ShowPauseMenu();
        if (gameIsPaused) {
            resume();
            Debug.Log("resume");
        } else {
            pause();
            Debug.Log("pause");
        }
       }
    }

    void ShowPauseMenu () {
            Cursor.visible = true;
            cursorState.cursorState = true;
    }

    public void resume () {
pauseMenuUI.SetActive(false);
gameIsPaused = false;
playerMovement.enabled = true;
Camerabob.enabled = true;
cursorState.cursorState = false;
    }

    void pause () {
pauseMenuUI.SetActive(true);
// Time.timeScale = 1f;
playerMovement.enabled = false;
Camerabob.enabled = false;
gameIsPaused = true;
    }

    public void Quitgame() {
    SceneManager.LoadScene("Start menu");
}
}

