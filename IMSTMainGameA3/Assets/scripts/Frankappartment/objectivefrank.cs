using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectivefrank : MonoBehaviour
{
    public coffeemachine air;
    public luchtfilter filter;
    public frank Frank;
    public TextMeshProUGUI text;
    AudioSource audiosource;
    public AudioClip objectivegeluid;

    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Talk to Frank.";
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (Frank.newobjective == true) {
            text.text = "Objective: Set a cup of Coffee.";
            Debug.Log("newobjective");
            Frank.newobjective = false;
            audiosource.PlayOneShot(objectivegeluid);
        }

        if (air.airobjective == true) {
            text.text = "Objective: Turn on the airfilter";
            air.airobjective = false;
            audiosource.PlayOneShot(objectivegeluid);
        }

        if (filter.objectiveend == true){
            text.text = "Objective: Go play with Frank";
            filter.objectiveend = false;
            audiosource.PlayOneShot(objectivegeluid);
        }
        

    }
}
