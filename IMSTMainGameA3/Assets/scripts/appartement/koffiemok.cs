using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiemok : MonoBehaviour
{

     public GameObject UIUpdate; // Het gameobject met de Image-component
  public Transform player; // Spelerreferentie
    Animator animator;
    AudioSource audiosource;
    BoxCollider boxcollider;
    public AudioClip drinken;
    public koffiezetapparaat koffie;
    public Image telefoon;
    public TextMeshProUGUI text;
    public bool telefoonfrank;

    private bool hasBeenUsed = false;

    private float activationDistance = 5.0f;


    void Start()
    {
    animator = GetComponent<Animator>();
    audiosource = GetComponent<AudioSource>();
        boxcollider = GetComponent<BoxCollider>();
        boxcollider.enabled = false;
        telefoon.enabled = false;
        telefoonfrank = false;
        text.enabled = false;
        UIUpdate.SetActive(false);

    }

    void Update() {
        if (koffie.koffieklaar == true) {
            boxcollider.enabled = true;
        }


    if (player != null && koffie.koffieklaar == true)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance && hasBeenUsed == false)
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

            public void koffiedrinken() {
        if (hasBeenUsed == false)
        {
            animator.SetTrigger("drinken");
            audiosource.PlayOneShot(drinken);
            StartCoroutine(phone());
            hasBeenUsed = true;
        }
            }

                IEnumerator phone() {
        yield return new WaitForSeconds(5);
        telefoon.enabled = true;
        telefoonfrank = true;
        text.enabled = true;
        text.text = "Press F to awnser your phone!";
    

}

}
