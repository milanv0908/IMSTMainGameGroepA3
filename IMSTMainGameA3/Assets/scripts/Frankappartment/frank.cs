using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public bool beginend = false;
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
                        newobjective = true;
                        isused = true;
                }

                if (filter.ending == true) {
                        beginend = true;
                }
              
}

void Update() {
        //         if (heeftGepraat.hasInteracted == true)
        // {
        //     // Zet de positie terug naar de oorspronkelijke positie
        //     transform.position = originalPosition;
        // }

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

            // Bijwerk de rotatie van het UIUpdate-object om met de speler mee te draaien
            UIUpdate.transform.rotation = Quaternion.Euler(-90, player.rotation.eulerAngles.y, 0);
        }
}


}
