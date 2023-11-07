using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcamerascene2 : MonoBehaviour
{
    public frank Frank;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public busstation bus;
    public luchtfilter einde;
    public int Manager;
    public bool eind = false;
    public bool bus2;

    void Start()
    {
        Cam1();
    }

    private bool changed = true;

    void Cam1()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
                Camera3.SetActive(false);
        StartCoroutine(huts());
    }

    void Cam2()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
                Camera3.SetActive(false);
        eind = true;
    }

    void Update()
    {
        if (Frank.beginend == true)
        {
            Debug.Log("Frank.beginend is true"); // Debug statement
            Camera2.SetActive(false);
            Camera3.SetActive(true);
        }
    }

    IEnumerator huts()
    {
        yield return new WaitForSeconds(14);
        Camera1.SetActive(false);
        Camera2.SetActive(true);
        eind = true;
    }
}