using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiezetapparaat : MonoBehaviour
{

 public GameObject UIUpdate; // Het gameobject met de Image-component
  public Transform player; // Spelerreferentie
    public bool koffieklaar;
    private bool hasBeenUsed = false;
    public AudioClip koffiegeluid;
    Animator animator;
    AudioSource audiosource;

    private float activationDistance = 5.0f;
    void Start()
    {
    animator = GetComponent<Animator>();
    audiosource = GetComponent<AudioSource>();
    UIUpdate.SetActive(false);
    }

       void Update()
    {
    if (player != null)
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

    public void koffieZetten()
    {
        if (hasBeenUsed == false)
        {
            animator.SetTrigger("koffie");
            audiosource.PlayOneShot(koffiegeluid);
            StartCoroutine(koffie());
            hasBeenUsed = true;
            UIUpdate.SetActive(false);
        }
    }

    IEnumerator koffie() {
        yield return new WaitForSeconds(5);
        koffieklaar = true;

}
}
