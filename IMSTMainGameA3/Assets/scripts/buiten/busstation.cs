using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class busstation : MonoBehaviour
{
    public Image image;
    AudioSource audiosource;
    public AudioClip busvertrek;
    public AudioClip busaankomst;
    public GameObject UIUpdate; // Het gameobject met de Image-component
    public Transform player; // Spelerreferentie
    private float activationDistance = 5.0f;

    public bool playanimation;
    public mondmaskerdispenser mondmask;

    void Start() {
        image.enabled = false;
        audiosource = GetComponent<AudioSource>();
        UIUpdate.SetActive(false);
    }
public void wachtenopdebus() {
        if (mondmask.heeftmondmask == true){
            image.enabled = true;
            audiosource.PlayOneShot(busvertrek);
            StartCoroutine(frank());
            playanimation = true;
            Debug.Log("huts");
        }
    
    
}

       void Update()
    {
    if (player != null)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= activationDistance)
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

    IEnumerator frank() {
        yield return new WaitForSeconds(13f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        

}

}
