using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcamerascene2 : MonoBehaviour
{
        public frank Frank;
public GameObject Camera1;
public GameObject Camera2;
        public GameObject Camera3;
public bool eind = false;
public int Manager;


        void Start(){

                StartCoroutine(nieuwescene());
                // Cam1();
        }



    private bool changed = true;

public void ManageCamera() {
    if(Manager == 0) {
            Cam2();
            Manager = 1;
    } else {
            Cam1();
            Manager = 0;
    }
}

// void Update() {
//         if (bus.playanimation == true && changed== true)
//         {
//             GetComponent<Animator>().SetTrigger("change");
//             changed = false;

//         }
// }

void Cam1() {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        Camera3.SetActive(false);
                Debug.Log("Hoi");
}

void Cam2() {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
        Camera3.SetActive(false);
        Debug.Log("Hoi2");
}

IEnumerator nieuwescene() {
    yield return new WaitForSeconds(14);
    eind = true; // Set the existing variable to true
    Cam2();
}

void Update() {
                if (Frank.beginend == true)
                Camera1.SetActive(false);
                Camera2.SetActive(false);
                Camera3.SetActive(true);

}

}
