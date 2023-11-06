using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gert : MonoBehaviour
{
    public bool gertI = false;
    void interact()
    {
        gertI = true;
        StartCoroutine(uit());
    }

                          IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        gertI = false;
    

}
}
