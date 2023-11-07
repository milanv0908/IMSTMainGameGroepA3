using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suzanne : MonoBehaviour
{
    public bool Suzanne = false;
    private bool interact = false;
public void Interact() {
   if(interact == false) {
            Suzanne = true;
            interact = false;
            StartCoroutine(uit());
   }
}

    IEnumerator uit() {
        yield return new WaitForSeconds(1);
        Suzanne = false;

}
}
