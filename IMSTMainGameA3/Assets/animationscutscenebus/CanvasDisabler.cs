using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDisabler : MonoBehaviour
{

    public bus buss;

    private Canvas canvas; // Reference to the Canvas component.

    private void Start()
    {
        // Get the Canvas component attached to this GameObject.
        canvas = GetComponent<Canvas>();

        // Ensure the Canvas component is not null.
        if (canvas == null)
        {
            Debug.LogError("Canvas component not found on the GameObject.");
        }
    }

    private void Update()
    {
        // Check the playerd boolean and enable/disable the Canvas accordingly.
        canvas.enabled = !buss.playerd;
    }
}
