using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public TV tv;
    public Image image;

void Start() {
        image.enabled = false;
    }
    public void restar() {
        if (tv.leave == false) {
            SceneManager.LoadScene("mainmenu");
            
        }
    }

    void Update() {
        if(tv.leave == false) {
            image.enabled = true;
            Debug.Log("hop");
        }
    }


}
