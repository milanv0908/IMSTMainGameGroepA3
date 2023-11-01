using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepscriptsimple : MonoBehaviour
{
    PlayerMove playermove;
    public GameObject Footstep;

    void Start()
    {
        playermove = GetComponent<PlayerMove>();
        Footstep.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKey("w"))
        {
            footsteps();
        }

        if(Input.GetKeyDown("s"))
        {
            footsteps();
        }

        if(Input.GetKeyDown("a"))
        {
            footsteps();
        }

        if(Input.GetKeyDown("d"))
        {
            footsteps();
        }

        if(Input.GetKeyUp("w"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("s"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("a"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("d"))
        {
            StopFootsteps();
        }

        if(Input.GetKeyUp("space"))
        {
            StopFootsteps();
        }


if (!playermove.isGrounded())
{
    StopFootsteps();
}

    }

    void footsteps()
    {
        Footstep.SetActive(true);
    }

    void StopFootsteps()
    {
        Footstep.SetActive(false);
    }
}
