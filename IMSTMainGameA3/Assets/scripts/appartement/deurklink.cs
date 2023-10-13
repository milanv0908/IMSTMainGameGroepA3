using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deurklink : MonoBehaviour
{
    Animator animator;
    public bool isActivated;
    void Start()
    {
    animator = GetComponent<Animator>();
    isActivated = false;
    }
public void activate() {
animator.SetTrigger("activate");
        isActivated = true;
        StartCoroutine(openDeur());
}

IEnumerator openDeur() {
        yield return new WaitForSeconds(0.1f);
        isActivated = false;

}
}
