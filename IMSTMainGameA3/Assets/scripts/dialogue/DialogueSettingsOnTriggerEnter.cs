using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueSettingsOnTriggerEnter : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player;

    public Rigidbody playerRigidbody; // Voeg een verwijzing naar de Rigidbody van de speler toe.

    private void Start()
    {
        // Haal een verwijzing naar de Rigidbody van de speler op
        playerRigidbody = player.GetComponent<Rigidbody>();

        // Pas de lineaire en angulaire demping van de Rigidbody aan
        playerRigidbody.drag = 5.0f; // Lineaire demping
        playerRigidbody.angularDrag = 5.0f; // Angulaire demping
    }

    private void Update()
    {
        // De rest van je Update-logica hier...
    }

    public void OnTriggerEnter(Collider other)
    {
        
         StartCoroutine(waitForTutorial());
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }

    IEnumerator waitForTutorial(){
        yield return new WaitForSeconds(3);
    }
}
