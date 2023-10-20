using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class koffiezetapparaat : MonoBehaviour
{
    public Image telefoon;
    public TextMeshProUGUI text;
    public bool telefoonfrank;
    Animator animator;
    void Start()
    {
    animator = GetComponent<Animator>();
        telefoon.enabled = false;
        telefoonfrank = false;
        text.enabled = false;
    }

    public void koffieZetten()
    {
        animator.SetTrigger("koffie");
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
