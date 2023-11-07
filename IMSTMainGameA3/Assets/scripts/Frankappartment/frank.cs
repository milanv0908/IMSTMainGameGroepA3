using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frank : MonoBehaviour
{
        public luchtfilter filter;
public bool hasinteracted1 = false;
public bool newobjective = false;
public bool isused = false;

        public bool beginend = false;

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
