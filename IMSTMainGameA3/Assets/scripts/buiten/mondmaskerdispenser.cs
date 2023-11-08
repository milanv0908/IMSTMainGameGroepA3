using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mondmaskerdispenser : MonoBehaviour
{
    public suzanne heeftGepraat;
    public Image mondmask;
    public Image geenmondmask;

    public GameObject UIUpdate;
    private float activationDistance = 10.0f;
    public Transform player;
    public bool hasinteracted = false;
    public bool heeftmondmask = false;
    public bool objectivebus = false;
    

    private Vector3 originalPosition; // Variabele om de oorspronkelijke positie op te slaan

    void Start()
    {
        UIUpdate.SetActive(false);

        // Sla de oorspronkelijke positie op bij het starten
        originalPosition = transform.position;
        // Stel de Z-positie in op -1000
        transform.position = new Vector3(transform.position.x, transform.position.y, -1000);
    }

    public void mondmasker()
    {
        mondmask.enabled = true;
        geenmondmask.enabled = false;
        heeftmondmask = true;
        hasinteracted = true;
        objectivebus = true;
        StartCoroutine(bloep());
    }

    void Update()
    {
        if (heeftGepraat.hasInteracted == true)
        {
            // Zet de positie terug naar de oorspronkelijke positie
            transform.position = originalPosition;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= activationDistance && hasinteracted == false)
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

    IEnumerator bloep()
    {
        yield return new WaitForSeconds(0.1f);
        objectivebus = false;
    }
}
