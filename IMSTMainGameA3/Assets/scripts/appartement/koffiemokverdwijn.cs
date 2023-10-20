using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koffiemokverdwijn : MonoBehaviour
{
    private MeshRenderer meshRenderer; // Reference to the MeshRenderer component

    public koffiezetapparaat koffie;

    void Start()
    {
        // Get the MeshRenderer component attached to this GameObject
        meshRenderer = GetComponent<MeshRenderer>();

        // Check if we found the MeshRenderer
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found on this object.");
        }
    }

    void Update()
    {
        if (koffie.koffieklaar == true && meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }
}
