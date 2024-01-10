using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class dialoguesettings : MonoBehaviour
{
    public Camerabob Camerabob;
    public NPCConversation Conversation;
    public speler player;
    public ObjectInteraction scriptToDisable;
    public PlayerMove playerMovement;

    private void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                scriptToDisable.enabled = false;
                playerMovement.enabled = false;
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
                scriptToDisable.enabled = true;
                playerMovement.enabled = true;
                Camerabob.enabled = true;
            }
        }
    }

    public void interactieTest1()
    {
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
}
