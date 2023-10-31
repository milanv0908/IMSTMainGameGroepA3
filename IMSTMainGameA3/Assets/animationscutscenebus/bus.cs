using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bus : MonoBehaviour
{
    public busstation buss;
    Animator animator;
    public bool playerd;
 
    void Start()
    {
    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
    animator = GetComponent<Animator>();
    playerd = false;    
    }


    void Update()
    {
       if (buss.playanimation == true) {
       GetComponent<MeshRenderer>().enabled = true;
       GetComponent<BoxCollider>().enabled = true;
       animator.SetTrigger("startanimatie");
       playerd = true;

       } 
    }
}
