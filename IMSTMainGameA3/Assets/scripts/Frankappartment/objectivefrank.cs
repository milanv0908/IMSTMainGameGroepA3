using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class objectivefrank : MonoBehaviour
{
    public frank Frank;
    public TextMeshProUGUI text;
    void Start()
    {
        text.enabled = true;
        text.text = "Objective: Talk to Frank.";
    }

    void Update()
    {
        if (Frank.newobjective = true) {
            text.text = "Objective: Set a cup of Coffee.";
            Debug.Log("newobjective");
        }
    }
}
