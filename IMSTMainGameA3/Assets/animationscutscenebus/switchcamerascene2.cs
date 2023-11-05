using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcamerascene2 : MonoBehaviour
{
public GameObject Camera1;
public GameObject Camera2;
public int Manager;

        public bool bus2;

        void Start(){
                bus2 = false;
                StartCoroutine(nieuwescene());
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
}

void Cam2() {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
}

                IEnumerator nieuwescene() {
        yield return new WaitForSeconds(8);
        ManageCamera();

}

}
