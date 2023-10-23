using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objectivescript1 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public koffiezetapparaat koffiedrinken;
    public koffiemok koffiemok;
    private bool textchanged1 = false;
    private bool textchanged2 = false;

    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Set a cup of Coffee";
    }

    void Update()
    {
        if (koffiedrinken.koffieklaar == true && !textchanged1)
        {
            text.text = "Objective: Drink the cup of coffee";
            textchanged1 = true;
        }

        if (koffiemok.telefoonfrank == true && !textchanged2)
        {
            text.text = "Objective: Answer the phone and listen to the caller";
            textchanged2 = true;
        }
    }
}
