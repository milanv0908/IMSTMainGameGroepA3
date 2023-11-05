using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williambellen : MonoBehaviour
{
    public bool dooropen = false;

    Animator animator;

    void Start()
    {
        StartCoroutine(aanbellen());
        animator = GetComponent<Animator>();
    }

                      IEnumerator aanbellen() {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("ring");
        yield return new WaitForSeconds(10);
        dooropen = true;
        
    

}
}
