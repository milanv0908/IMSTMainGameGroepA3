using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spelertelefoon : MonoBehaviour
{
    public koffiezetapparaat koffie;
    public Image telefoon;

    void Update()
    {
        // Check if the "telefoonfrank" variable is true in the "koffie" object.
        if (koffie.telefoonfrank == true && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("hoi");
            telefoon.enabled = false;
        }
    }
}
