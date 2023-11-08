using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class coffeemachine : MonoBehaviour
{
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie
    public frank frank;

    public bool lekkerkoffie = false;
    public bool airobjective = false;
    public bool poep = false;
    AudioSource audiosource;
    private float activationDistance = 5.0f;

    void Start() {
    GetComponent<BoxCollider>().enabled = false;
    audiosource = GetComponent<AudioSource>();
    UIUpdate.SetActive(false);
        poep = false;
    }

    public void interact() {
        lekkerkoffie = true;
        poep = true;

    }

    void Update() {
        if (frank.hasinteracted1 == true) {
GetComponent<BoxCollider>().enabled = true;
        }
        if(lekkerkoffie == true) {
            GetComponent<BoxCollider>().enabled = false;

        }

    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && frank.hasinteracted1 == true && lekkerkoffie == false)
        {
            UIUpdate.SetActive(true);
        }
        else
        {
            UIUpdate.SetActive(false);
        }

        // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
        UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        //  UIUpdate.transform.rotation = Quaternion.Euler(270, player.rotation.eulerAngles.z, 0);
        
        // Reset de schaal naar positieve waarden
        // UIUpdate.transform.localScale = new Vector3(1, 1, 1);
    }

         if (!ConversationManager.Instance.IsConversationActive && poep == true)
        {
            airobjective = true;
            poep = false;
        }
    }
}
