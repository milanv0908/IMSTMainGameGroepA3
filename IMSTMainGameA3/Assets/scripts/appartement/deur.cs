// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class deur : MonoBehaviour
// {
//     Animator animator;
//     deurklink deurklink;
//     public bool isopen;
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         isopen = false;
//     }
//     public void Update()
//     {
//         if (isopen == false && deurklink.isActivated == true)
//         {
//             animator.SetTrigger("open");
//             isopen = true;
//         }

//         if (isopen == true && deurklink.isActivated == true) {
//             animator.SetTrigger("sluit");
//             isopen = false;
//         }
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class deur : MonoBehaviour
// {
//     Animator animator;
//     public deurklink deurklink;
//     public bool isopen;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         isopen = false;
//     }

//     public void Update()
//     {
//             if (isopen == false && deurklink.isActivated == true)
//             {
//                 StartCoroutine(isopen1());

//             }

//             if (isopen == true && deurklink.isActivated == true)
//             {
    
//                 StartCoroutine(isdicht());

//             }
//         }

//         IEnumerator isopen1() {
//         yield return new WaitForSeconds(0.1f);
//         animator.SetTrigger("open");
//         isopen = true;
//         Debug.Log("open");

    

// }

//         IEnumerator isdicht() {
//         yield return new WaitForSeconds(0.1f);
//         animator.SetTrigger("sluit");
//         isopen = false;
//         Debug.Log("Sluiten");


// }


// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deur : MonoBehaviour
{
    Animator animator;
    public deurklink deurklink;
    public bool isopen;
    private bool isCoroutineRunning = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        isopen = false;
    }

    public void Update()
    {
        if (isopen == false && deurklink.isActivated == true && !isCoroutineRunning)
        {
            StartCoroutine(isopen1());
        }

        if (isopen == true && deurklink.isActivated == true && !isCoroutineRunning)
        {
            StartCoroutine(isdicht());
        }
    }

    IEnumerator isopen1()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("open");
        isopen = true;
        Debug.Log("open");
        isCoroutineRunning = false;
    }

    IEnumerator isdicht()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("sluit");
        isopen = false;
        Debug.Log("Sluiten");
        isCoroutineRunning = false;
    }
}