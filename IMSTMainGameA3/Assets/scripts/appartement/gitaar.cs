using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gitaar : MonoBehaviour
{
        public GameObject UIUpdate; // Het gameobject met de Image-component
    public koffiemok koffiemok;
    private float activationDistance = 5.0f;
    public Transform player; // Spelerreferentie
    public bool pickedupguitar;

    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
        UIUpdate.SetActive(false); 
    }

public void Gitaaropakken() {   
GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
        pickedupguitar = true;

}
    void Update()
    {
            // if (koffiemok.hasBeenUsed == true) {
    //     GetComponent<BoxCollider>().enabled = true;
    //     UIUpdate.SetActive(true);

    // }

        if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && koffiemok.hasBeenUsed == true && pickedupguitar == false)
        {
            UIUpdate.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
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
    }
}
