using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mondmaskerdispenser : MonoBehaviour
{
    public Image mondmask;
    public Image geenmondmask;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    private float activationDistance = 5.0f;
    public Transform player; // Spelerreferentie

    public bool heeftmondmask = false;

    void Start() {
    UIUpdate.SetActive(false);
    }

public void mondmasker() {
    mondmask.enabled = true;
    geenmondmask.enabled = false;
    heeftmondmask = true;


}

void Update() {
                    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance)
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
