using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mondmaskerdispenser : MonoBehaviour
{
    public Image mondmask;
    public Image geenmondmask;

public void mondmasker() {
    mondmask.enabled = true;
        geenmondmask.enabled = false;

}
}
