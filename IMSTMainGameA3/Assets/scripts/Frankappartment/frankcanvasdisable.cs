using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class frankcanvasdisable : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas component. Assign the Canvas in the Inspector.
    public frank Frank;
    public switchcamerascene2 jot;

    void Start()
    {
        // Ensure that the canvas component is assigned in the Inspector.
        if (canvas != null)
        {
            canvas.enabled = false;
        }
        else
        {
            Debug.LogError("Canvas component is not assigned. Please assign it in the Inspector.");
        }
    }

    private void Update()
    {
        if (jot.eind == true && canvas != null && Frank.beginend == false)
        {
            canvas.enabled = true;
        }

        if (Frank.beginend == true) {
            canvas.enabled = false;
        }
    }
}
