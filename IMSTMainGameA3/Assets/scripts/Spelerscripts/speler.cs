using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class speler : MonoBehaviour
{
    public bool heeftPlayer2Gekozen;
    public bus buss;

    private int currentSceneIndex; // Variabele om de huidige scène-index bij te houden

    void Start()
    {
        heeftPlayer2Gekozen = false;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Haal de huidige scène-index op
    }

    void Update()
    {
        // Controleer of we in "Scene 2" zijn voordat we de code uitvoeren
        if (currentSceneIndex == 2 && buss.playerd == true)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
