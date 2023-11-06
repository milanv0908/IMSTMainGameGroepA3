using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetevent : MonoBehaviour
{
    public bool hasinteracted = false;

    void OnTriggerEnter()
    {
        hasinteracted = true;
        StartCoroutine(bloep());
        
    }

                        IEnumerator bloep() {
        yield return new WaitForSeconds(0.1f);
        hasinteracted = false;
    

}
  

}
