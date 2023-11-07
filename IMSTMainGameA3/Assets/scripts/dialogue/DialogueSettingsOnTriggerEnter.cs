using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueSettingsOnTriggerEnter : MonoBehaviour
{
    public NPCConversation Conversation;
    public speler player; // 'speler' veranderd naar 'Speler'

    private Rigidbody playerRigidbody;
    private float originalDrag;
    private float originalAngularDrag;
    private BoxCollider boxCollider;

    private void Start()
    {
        // Haal een verwijzing naar de Rigidbody van de speler op
        playerRigidbody = player.GetComponent<Rigidbody>();
        originalDrag = playerRigidbody.drag;
        originalAngularDrag = playerRigidbody.angularDrag;

        // Haal een verwijzing naar de BoxCollider op
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // De rest van je Update-logica hier...
    }

    public void OnTriggerEnter(Collider other)
    {
        playerRigidbody.drag = 5.0f; // Lineaire demping
        playerRigidbody.angularDrag = 5.0f; // Angulaire demping

        StartCoroutine(WaitForTutorial());
        if (!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }

        // Schakel de BoxCollider uit
        boxCollider.enabled = false;
    }

    IEnumerator WaitForTutorial()
    {
        yield return new WaitForSeconds(3);

        // Herstel de oorspronkelijke waarden van de Rigidbody na een vertraging
        playerRigidbody.drag = originalDrag;
        playerRigidbody.angularDrag = originalAngularDrag;
    }
}
