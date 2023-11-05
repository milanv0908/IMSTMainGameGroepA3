using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffeemachine : MonoBehaviour
{
    public frank frank;

    void Start() {
    GetComponent<BoxCollider>().enabled = false;
    }

    void Update() {
        if (frank.hasinteracted == true) {
GetComponent<BoxCollider>().enabled = true;
        }
    }
}
