using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class blokkieInteract : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player;
    public ObjectInteraction scriptToDisable; // Voeg een referentie naar het script dat je wilt uitschakelen toe.
    public PlayerMove playerMovement; // Voeg een referentie naar het bewegingsscript van de speler toe.

    private void Update()
    {
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                // Schakel het script uit wanneer de conversatie actief is
                scriptToDisable.enabled = false;
                // Schakel de beweging van de speler uit
                playerMovement.enabled = false;

                // Voer de rest van je dialooglogica uit
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();

                // Controleer of de bool "kies2" true is
                bool kies2Value = ConversationManager.Instance.GetBool("kies2");
                if (kies2Value)
                {
                    player.heeftPlayer2Gekozen = true;
                }
                else
                {
                    player.heeftPlayer2Gekozen = false;
                }
            }
            else
            {
                // Zorg ervoor dat het script weer wordt ingeschakeld wanneer de conversatie niet actief is
                scriptToDisable.enabled = true;
                // Schakel de beweging van de speler weer in
                playerMovement.enabled = true;

                // Voer andere logica uit wanneer de conversatie niet actief is
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
