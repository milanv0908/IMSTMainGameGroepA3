using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objectivescript1 : MonoBehaviour
{
    AudioSource audiosource;
    public TextMeshProUGUI text;
    public koffiezetapparaat koffiedrinken;
    public koffiemok koffiemok;
    public AudioClip objectivegeluid;
    public spelertelefoon spelerbool;
    private bool textchanged1 = false;
    private bool textchanged2 = false;

    private bool textchanged3 = false;


    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Set a cup of Coffee";
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (koffiedrinken.koffieklaar == true && !textchanged1)
        {
            text.text = "Objective: Drink the cup of coffee";
            textchanged1 = true;
            audiosource.PlayOneShot(objectivegeluid);

        }

        if (koffiemok.telefoonfrank == true && !textchanged2)
        {
            text.text = "Objective: Answer the phone and listen to the caller";
            textchanged2 = true;
        }

            if (spelerbool.DeurOpen == true && !textchanged3)
        {
            text.text = "Objective: Find you're AirMask and leave your appartment";
            textchanged3 = true;
            audiosource.PlayOneShot(objectivegeluid);
        }

        
    }
}
