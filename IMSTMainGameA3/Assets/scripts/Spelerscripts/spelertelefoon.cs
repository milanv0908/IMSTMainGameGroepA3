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

    bool heeftOpgehangen;
    private bool runOnce = false;
    public bool DeurOpen = false;

    void Start()
    {
        runOnce = false;
    }

    void Update()
    {
        if (koffie.telefoonfrank == true && Input.GetKeyDown(KeyCode.F))
        {
            telefoon.enabled = false;
            koffie.telefoonfrank = false;
            text.enabled = false;
            heeftOpgehangen = true;
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
}
