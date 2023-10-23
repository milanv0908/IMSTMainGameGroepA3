using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiezetapparaat : MonoBehaviour
{

    public bool koffieklaar;
    public AudioClip koffiegeluid;
    Animator animator;
    AudioSource audiosource;
    void Start()
    {
    animator = GetComponent<Animator>();
    audiosource = GetComponent<AudioSource>();
    }

    public void koffieZetten()
    {
        animator.SetTrigger("koffie");
        audiosource.PlayOneShot(koffiegeluid);
        StartCoroutine(koffie());
    }

    IEnumerator koffie() {
        yield return new WaitForSeconds(5);
        koffieklaar = true;

}
}
