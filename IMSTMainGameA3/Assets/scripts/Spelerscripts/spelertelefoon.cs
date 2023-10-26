using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueEditor;

public class spelertelefoon : MonoBehaviour
{

    public NPCConversation Conversation;
    // public speler player;
    // public ObjectInteraction scriptToDisable; // Voeg een referentie naar het script dat je wilt uitschakelen toe.
    public PlayerMove playerMovement; // Voeg een referentie naar het bewegingsscript van de speler toe.
    public Camerabob Camerabob; // Voeg een referentie naar het bewegingsscript van de speler toe.

    public koffiemok koffie;
    public Image telefoon;
    public TextMeshProUGUI text;

    bool heeftOpgehangen;

     private bool runOnce = false;

    void Start()
    {
        // Set the runOnce boolean to false
        runOnce = false;
    }

    void Update()
    {
        
        // Check if the "telefoonfrank" variable is true in the "koffie" object.
        if (koffie.telefoonfrank == true && Input.GetKeyDown(KeyCode.F))
        {
            telefoon.enabled = false;
            koffie.telefoonfrank = false;
            text.enabled = false;
            heeftOpgehangen = true;
        }

            if(runOnce == false){
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
                // Schakel het script uit wanneer de conversatie actief is
                // scriptToDisable.enabled = false;
                // Schakel de beweging van de speler uit
                playerMovement.enabled = false;

                Camerabob.enabled = false;

                // Voer de rest van je dialooglogica uit
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.E))
                    ConversationManager.Instance.PressSelectedOption();

            }
            else
            {
                // Zorg ervoor dat het script weer wordt ingeschakeld wanneer de conversatie niet actief is
                // scriptToDisable.enabled = true;
                // Schakel de beweging van de speler weer in
                playerMovement.enabled = true;
                
                Camerabob.enabled = true;

                // Voer andere logica uit wanneer de conversatie niet actief is
            }
         }
        
    }
}
