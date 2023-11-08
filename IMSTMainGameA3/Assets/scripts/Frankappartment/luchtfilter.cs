using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class luchtfilter : MonoBehaviour
{
    public GameObject UIUpdate; // The GameObject with the Image component
    public Transform player; // Player reference
    public coffeemachine koffie; // Assuming coffeemachine is the correct class
    public bool ending = false;
    public bool objectiveend = false;
    private float activationDistance = 5.0f;
    public NPCConversation Conversation;
    private bool poep;

    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    public void Interact() {
        
        ending = true;
    
        poep = true;
    }

    void Update()
    {
        if (koffie.lekkerkoffie == true)
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= activationDistance && koffie.lekkerkoffie == true && ending == false)
            {
                UIUpdate.SetActive(true);
            }
            else
            {
                UIUpdate.SetActive(false);
            }

            // Update the rotation of the UIUpdate object to follow the player's rotation
            UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);

            // Reset the scale to positive values
            UIUpdate.transform.localScale = new Vector3(1, 1, 1);
        }

                 if (!ConversationManager.Instance.IsConversationActive && poep == true)
        {
            poep = false;
            objectiveend = true;
        }
    }
}
