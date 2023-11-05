using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jonas : MonoBehaviour
{
    public bool jonasinteract = false;
    public void jonas()
    {
        jonasinteract = true;
        StartCoroutine(bloep());
    }
IEnumerator bloep() {
 yield return new WaitForSeconds(0.1f);
jonasinteract = false;
    

}

}
