using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frank : MonoBehaviour
{
        AudioSource audiosource;
public luchtfilter filter;
public bool hasinteracted1 = false;
public bool newobjective = false;
public bool isused = false;

public bool beginend = false;

void Start() {
        audiosource = GetComponent<AudioSource>();
        audiosource.enabled = false;
        StartCoroutine(audioisgone());
}
    IEnumerator audioisgone() {
        yield return new WaitForSeconds(13);
        audiosource.enabled = true;

}

public void interacting() {
                if (isused == false)
                {
                        hasinteracted1 = true;
                        newobjective = true;
                        isused = true;
                }

                if (filter.ending == true) {
                        beginend = true;
                }
              
}


}
