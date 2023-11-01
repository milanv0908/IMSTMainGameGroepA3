using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mondkapjebreekt : MonoBehaviour
{
    public Image geenmondkap;
    public Image mondkap;

    void Start()
    {
        mondkap.enabled = true;
        geenmondkap.enabled = false;
    }

    
     public void OnTriggerEnter(Collider other)
    {
     if (other.CompareTag("Player")){
        mondkap.enabled = false;
        geenmondkap.enabled = true;
     }
    }
}
