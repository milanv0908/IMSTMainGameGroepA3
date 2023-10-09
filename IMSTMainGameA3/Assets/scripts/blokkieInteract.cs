using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class blokkieInteract : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player;

    bool kies2;

    private void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.F))
                    ConversationManager.Instance.PressSelectedOption();

                // Controleer of de bool "kies2" true is
                bool kies2Value = ConversationManager.Instance.GetBool("kies2");
                if (kies2Value)
                {
                    // Debug.Log("kies2 is true");
                    player.heeftPlayer2Gekozen = true;
                }
                else
                {
                    // Debug.Log("kies2 is false");
                    player.heeftPlayer2Gekozen = false;
                }
            }
        }
    }

    public void interactieTest1()
    {
        ConversationManager.Instance.StartConversation(Conversation);
    }
}
