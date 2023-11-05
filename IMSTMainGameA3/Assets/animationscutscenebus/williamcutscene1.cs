using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williamcutscene1 : MonoBehaviour
{
    public bus bussie;
    Animator animator;

    void Start() {
animator = GetComponent<Animator>();
    }

    void Update() {
        if (bussie.playerd == true) {
            animator.SetTrigger("appear");
            StartCoroutine(dissapear());
            Debug.Log("Appear");
        }
    }

    IEnumerator dissapear() {
        yield return new WaitForSeconds(4);
        animator.SetTrigger("dissapear");
        Debug.Log("Dissapear");
        

}
}
