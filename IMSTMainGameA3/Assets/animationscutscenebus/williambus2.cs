using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class williambus2 : MonoBehaviour
{
    Animator animator;

    public bus2 bus;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      if (bus.william == true) {
        StartCoroutine(appear());
      }
    }

        IEnumerator appear() {
        yield return new WaitForSeconds(3);
        animator.SetTrigger("appear");
        Debug.Log("jot");
        

}
}
