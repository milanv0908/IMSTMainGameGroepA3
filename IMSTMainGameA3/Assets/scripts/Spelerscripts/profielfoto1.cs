using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class profielfoto1 : MonoBehaviour
{
    public Image profiel;
    public Image profielmetmask;

    public maskerscript maskopgepakt;



    void Start()
    {
        profiel.enabled = true;
        profielmetmask.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (maskopgepakt.maskeraan)
        {
            profiel.enabled = false;
            profielmetmask.enabled = true;
        }


    }
}
