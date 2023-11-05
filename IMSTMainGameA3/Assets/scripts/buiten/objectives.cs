using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectives : MonoBehaviour
{
    public TextMeshProUGUI text;
    public streetevent streetevent;
    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Walk to the busstation.";
    }

    // Update is called once per frame
    void Update()
    {
       if (streetevent.hasinteracted = true) {
            text.text = "Objective: Maybe somebody in park can tell you where to find a facemask.";
       } 
    }
}
