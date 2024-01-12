using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
    public bool image1off = false;
    public bool image2off = false;
    public Image image1;
    public Image image2;
    public Image image3;

    void Start()
    {
        image1.enabled = false;
        image2.enabled = true;
        image3.enabled = false;
    }

    void Update()
    {
       if (image1off == true) {
            image1.enabled = false;
            image2.enabled = true;
            image3.enabled = false;
            image1off = false;
       }
    }

    // Specify a return type for the knoppie method, assuming it's intended to be a void method
    public void knoppie() {
        image1off = true;
    }
}