using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class streetevent : MonoBehaviour
{
    public bool hasinteracted = false;
    public Image masker;
    public Image geenmasker;

    void OnTriggerEnter()
    {
        hasinteracted = true;
        StartCoroutine(bloep());
        masker.enabled = false;
        geenmasker.enabled = true;
        
    }

                        IEnumerator bloep() {
        yield return new WaitForSeconds(0.1f);
        hasinteracted = false;
    

}
  

}
