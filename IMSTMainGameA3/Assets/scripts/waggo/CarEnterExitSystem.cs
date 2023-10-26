// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class CarEnterExitSystem : MonoBehaviour
// {
//     public MonoBehaviour CarController2;
//     public GameObject Player;
//     bool Candrive;

//     void Start()
//     {
//         CarController2.enabled = false;
//     }

//     void Update()
//     {

//     }

//     void OnTriggerStay(Collider col)
//     {
//         if (col.gameObject.tag == "Player")
//         {
//             Candrive = true;
//         }
//     }

//     void OnTriggerExit(collider col)
//     {
//         if (col.gameObject.tag == "Player")
//         {
//             Candrive = false;
//         }
//     }

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterExitSystem : MonoBehaviour
{
    public MonoBehaviour CarController2;
    public Transform Player;
    public Transform Car;
    bool CanDrive;

    void Start()
    {
        CarController2.enabled = false;
    }

    void Update()
    {
    if (Input.GetKeyDown(KeyCode.F) && CanDrive) {
            player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);
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
