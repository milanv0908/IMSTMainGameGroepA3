using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepscript : MonoBehaviour
{
    public PlayerMove PlayerMove;
    private AudioSource audiosource;
    private float footstepTimer = 10; // You need to declare this variable.
    private float timePerStep = 4f;

    private enum TerrainTags
    {
wood,
stone

    }

    [SerializeField] private AudioClip[] footstepaudio;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

 private void Update()
{
    footstepTimer += Time.deltaTime;

    if (PlayerMove.isMoving && audiosource.clip && footstepTimer > timePerStep)
    {
        audiosource.Play();
        footstepTimer = 0;
    }
    else if (!PlayerMove.isMoving)
    {
        audiosource.Stop(); // Stop the audio when the player is not moving.
    }

    if (!PlayerMove.isGrounded())
    {
        audiosource.enabled = false;
    }
    else
    {
        audiosource.enabled = true;
    }
}


    private void OnCollisionEnter(Collision col)
    {
        int index = 0;
        foreach (string tag in Enum.GetNames(typeof(TerrainTags)))
        {
            if (col.gameObject.tag == tag)
            {
                audiosource.clip = footstepaudio[index];
            }
            index++;
        }
    }
}
