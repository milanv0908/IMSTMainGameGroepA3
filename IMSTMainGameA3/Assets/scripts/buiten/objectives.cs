using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectives : MonoBehaviour
{
    public TextMeshProUGUI text;
    public streetevent streetevent;
    public mondmaskerdispenser facemask;
     public suzanne activate;
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

       if (activate.Suzanne == true) {
               text.text = "Objective: Buy a facemask at the dispenser.";
       }

       if (facemask.objectivebus == true) {
            text.text = "Objective: Go to the busstation and wait for your ride";
       }
    }
}
