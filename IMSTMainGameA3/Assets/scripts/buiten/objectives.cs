using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectives : MonoBehaviour
{
    public TextMeshProUGUI text;
    public streetevent streetevent;
    public Jonas jonas;
    public mondmaskerdispenser facemask;
    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Walk to the busstation.";
    }

    // Update is called once per frame
    void Update()
    {
       if (streetevent.hasinteracted == true) {
            text.text = "Objective: Maybe somebody in park can tell you where to find a facemask.";
       }

       if (jonas.jonasinteract == true) {
            text.text = "Objective: Go to the FaceMask dispenser."; 
       }

       if (facemask.objectivebus == true) {
            text.text = "Objective: Go to the busstation and wait for youre ride";
       }
    }
}
