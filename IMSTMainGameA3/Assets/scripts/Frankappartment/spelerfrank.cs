using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spelerfrank : MonoBehaviour
{
    public frank Frank;
    void Update()
    {
     if (Frank.beginend == true) {
        GetComponent<MeshRenderer>().enabled = false;
     }   
    }
}
