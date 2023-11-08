using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueEditor;

public class spelertelefoon : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player;
    public Camerabob Camerabob;
    public koffiemok koffie;
    public Image telefoon;
    public TextMeshProUGUI text;

public AudioSource otherGameObjectAudioS;

public GameObject stappen;

    public Animator animator;
    private bool hasRunPopIn = false;
    private bool hasShowed;
    public Image image;
    public static bool gameIsPaused;


    bool heeftOpgehangen;
    private bool runOnce = false;
    public bool DeurOpen = false;

    void Start()
    {
        otherGameObjectAudioS = GetComponent<AudioSource>();
        runOnce = false;
                hasShowed = false;
        image.enabled = false;
    }

    void Update()
    {
        if (koffie.telefoonfrank == true && Input.GetKeyDown(KeyCode.F))
        {
            telefoon.enabled = false;
            koffie.telefoonfrank = false;
            text.enabled = false;
            heeftOpgehangen = true;

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

             if (Input.GetKeyDown(KeyCode.E) && gameIsPaused == true)
        {
            animator.SetTrigger("PopOut");
            // image.enabled = false;
            resume();
        }

        if (runOnce == false)
        {
            if (!ConversationManager.Instance.IsConversationActive && heeftOpgehangen == true)
            {
                ConversationManager.Instance.StartConversation(Conversation);
                runOnce = true;
            }
        }

        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                Camerabob.enabled = false;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();
            }
            else
            {
                Camerabob.enabled = true;
            }
        }

        if (!ConversationManager.Instance.IsConversationActive && heeftOpgehangen == true && !DeurOpen)
        {
            DeurOpen = true;
        }
    }




     public void resume()
    {
        // image.enabled = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
        otherGameObjectAudioS.enabled = true;
        stappen.SetActive(true);
    }

    void pause()
    {
        image.enabled = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
        otherGameObjectAudioS.enabled = false;
         stappen.SetActive(false);
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
