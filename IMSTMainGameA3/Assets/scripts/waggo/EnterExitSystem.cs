using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExitSystem : MonoBehaviour
{
    public MonoBehaviour CarController;
    public Transform Car;
    public Transform Player;

    [Header("Cameras")]
    public Camera PlayerCam;
    public Camera CarCam;

    bool CanDrive;

    // Start is called before the first frame update
    void Start()
    {
        CarController.enabled = false;
        CarCam.enabled = false;
        PlayerCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CanDrive)
        {
            CarController.enabled = true;

            Player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);

            // Switch to the car camera
            CarCam.enabled = true;
            PlayerCam.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            CarController.enabled = false;

            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);

            // Switch to the player camera
            CarCam.enabled = false;
            PlayerCam.enabled = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CanDrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CanDrive = false;
        }
    }
}