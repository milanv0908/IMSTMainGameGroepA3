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

    
    void OnTriggerEnter()
    {
        mondkap.enabled = false;
        geenmondkap.enabled = true;
    }
}
