using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nieuweshits : MonoBehaviour
{
void OnTriggerEnter() {
    SceneManager.LoadScene("ExpansionPack");
}
}
