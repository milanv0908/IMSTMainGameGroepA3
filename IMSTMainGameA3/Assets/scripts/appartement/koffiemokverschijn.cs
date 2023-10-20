using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koffiemokverschijn : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public koffiezetapparaat koffie;
    void Start()
    {
                // Get the MeshRenderer component attached to this GameObject
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        // Check if we found the MeshRenderer
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found on this object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
             if (koffie.koffieklaar == true && meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }   
    }
}
