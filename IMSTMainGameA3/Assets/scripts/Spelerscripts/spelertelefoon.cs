using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spelertelefoon : MonoBehaviour
{
    public koffiezetapparaat koffie;
    public Image telefoon;
    public TextMeshProUGUI text;

    void Update()
    {
        // Check if the "telefoonfrank" variable is true in the "koffie" object.
        if (koffie.telefoonfrank == true && Input.GetKeyDown(KeyCode.F))
        {
            telefoon.enabled = false;
            koffie.telefoonfrank = false;
            text.enabled = false;
        }
    }
}
