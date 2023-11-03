using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bus2 : MonoBehaviour
{
    Animator animator;
    public switchcamera playerbool;
    
 
    void Start()
    {
    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<BoxCollider>().enabled = false;
    animator = GetComponent<Animator>();
        
    }


    void Update()
    {
       if (playerbool.bus2 == true) {
       GetComponent<MeshRenderer>().enabled = true;
       GetComponent<BoxCollider>().enabled = true;
       animator.SetTrigger("start2");
       Debug.Log("hoi");
       

       } 
    }
}
