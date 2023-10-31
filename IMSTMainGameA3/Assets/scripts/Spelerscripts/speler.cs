using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speler : MonoBehaviour
{
 public bool heeftPlayer2Gekozen;

   public bus buss;

 void Start(){
    heeftPlayer2Gekozen = false;
 }

 void Update() {
   if(buss.playerd == true) {
      StartCoroutine(spelerverdwijn());
   }

       IEnumerator spelerverdwijn() {
        yield return new WaitForSeconds(3);
        GetComponent<MeshRenderer>().enabled = false;
      
        

}
 }
}
