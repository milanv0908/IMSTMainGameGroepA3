using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepscript : MonoBehaviour
{
    public PlayerMove PlayerMove;
    private AudioSource audiosource;
    private float footstepTimer = 0.0f; // You need to declare this variable.
    private float timePerStep = 5.3f;

    private enum TerrainTags
    {
wood,
stone,
grass,

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
            // Debug.Log("voetstappen");
    }
    else if (!PlayerMove.isMoving)
    {
        audiosource.Stop(); // Stop the audio when the player is not moving.
            // Debug.Log("geen voetstappen");
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
    string currentTag = col.gameObject.tag;

    for (int index = 0; index < footstepaudio.Length; index++)
    {
        if (currentTag == Enum.GetNames(typeof(TerrainTags))[index])
        {
            // Check if the current audio clip is different from the new one.
            if (audiosource.clip != footstepaudio[index])
            {
                audiosource.clip = footstepaudio[index];
                audiosource.Play(); // Start playing the new audio clip.
            }
            return; // Exit the loop since we found a matching tag.
        }
    }
}

}
