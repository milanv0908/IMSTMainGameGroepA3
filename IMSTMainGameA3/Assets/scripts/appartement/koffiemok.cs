using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiemok : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;
    BoxCollider boxcollider;
    public AudioClip drinken;
    public koffiezetapparaat koffie;
    public Image telefoon;
    public TextMeshProUGUI text;
    public bool telefoonfrank;


    void Start()
    {
    animator = GetComponent<Animator>();
    audiosource = GetComponent<AudioSource>();
        boxcollider = GetComponent<BoxCollider>();
        boxcollider.enabled = false;
        telefoon.enabled = false;
        telefoonfrank = false;
        text.enabled = false;

    }

    void Update() {
        if (koffie.koffieklaar == true) {
            boxcollider.enabled = true;
        }
    }

            public void koffiedrinken() {
                animator.SetTrigger("drinken");
                audiosource.PlayOneShot(drinken);
                StartCoroutine(phone());
            }

                IEnumerator phone() {
        yield return new WaitForSeconds(5);
        telefoon.enabled = true;
        telefoonfrank = true;
        text.enabled = true;
        text.text = "Press F to awnser your phone!";
    

}

}
