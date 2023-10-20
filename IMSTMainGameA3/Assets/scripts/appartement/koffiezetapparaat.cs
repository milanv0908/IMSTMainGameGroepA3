using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiezetapparaat : MonoBehaviour
{

    public bool koffieklaar;
    Animator animator;
    void Start()
    {
    animator = GetComponent<Animator>();
    }

    public void koffieZetten()
    {
        animator.SetTrigger("koffie");
        StartCoroutine(koffie());
    }

    IEnumerator koffie() {
        yield return new WaitForSeconds(5);
        koffieklaar = true;

}
}
