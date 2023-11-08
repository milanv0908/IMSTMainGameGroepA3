using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class frank : MonoBehaviour
{

public GameObject UIUpdate;
private float activationDistance = 10.0f;
public Transform player;
AudioSource audiosource;
public luchtfilter filter;
public bool hasinteracted1 = false;
public bool newobjective = false;
public bool isused = false;
private bool isused1 = false;
        public bool animation = false;

public bool beginend = false;
        private bool poop = false;

public NPCConversation Conversation;
private Vector3 originalPosition; // Variabele om de oorspronkelijke positie op te slaan

void Start() {
        audiosource = GetComponent<AudioSource>();
        audiosource.enabled = false;
        StartCoroutine(audioisgone());
        UIUpdate.SetActive(false);
}
    IEnumerator audioisgone() {
        yield return new WaitForSeconds(13);
        audiosource.enabled = true;

}

public void interacting() {
                if (isused == false)
                {
                        hasinteracted1 = true;
                        isused = true;
                        poop = true;
                }

                if (filter.ending == true) {
                        beginend = true;
                        UIUpdate.SetActive(false);
                        filter.ending = false;
                        animation = true;
                }
              
}

void Update()
{
    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && isused == false)
        {
            UIUpdate.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Check if filter.ending is true and activate UIUpdate accordingly
        if (filter.ending == true)
        {
            UIUpdate.SetActive(true);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
    }

                if (!ConversationManager.Instance.IsConversationActive && poop == true)
        {
            newobjective = true;
                poop = false;
        }
}


}
