using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suzanne : MonoBehaviour
{
   public bool hasInteracted = false;
    public bool Suzanne = false;
    private bool interact = false;


    private float activationDistance = 10.0f;

    bool hasBeenUsed = false;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie



     void Update(){
         if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && hasInteracted == false)
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
     }

public void Interact() {
   if(interact == false) {
            Suzanne = true;
            interact = false;
            hasInteracted = true;
            bool hasBeenUsed = true;
            StartCoroutine(uit());
   }
}

    IEnumerator uit() {
        yield return new WaitForSeconds(1);
        Suzanne = false;

}
}
