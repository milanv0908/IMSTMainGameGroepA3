using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footSteps : MonoBehaviour
{

  private AudioSource audioSource;
  private GameObject terrainFoot;
  private AudioSource terrainFootPrev;
  private AudioSource terrainFootNext;

  // LIST OF TERRAINS
  public AudioSource footstepGrass;  // 1
  public AudioSource footstepFloor;
  public AudioSource footstepSand;
  public AudioSource footstepWater;
  public AudioSource footstepSnow;
  public AudioSource footstepBridge;

  private void Start(){
    terrainFootNext = terrainFootPrev = footstepGrass;  // PLAYER STARTING TERRAIN
  }


  void Update()
  {
    audioSource = GetComponent<AudioSource>();

    terrainFoot = FindObjectOfType<TerrainDetector>().PlayerTerrain();


        if( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)  )
            {

              switch(terrainFoot.name)
              {
                    case "GRASS":
                          footstepGrass.enabled   = true;   // 2
                          terrainFootPrev = footstepGrass;  // 3
                          break;
                    case "floor":
                          footstepFloor.enabled   = true;
                          terrainFootPrev = footstepFloor;
                          break;
                    case "SAND":
                          footstepSand.enabled   = true;
                          terrainFootPrev = footstepSand;
                          break;
                    case "WATER":
                          footstepWater.enabled   = true;
                          terrainFootPrev = footstepWater;
                          break;
                    case "SNOW":
                          footstepSnow.enabled   = true;
                          terrainFootPrev = footstepSnow;
                          break;
                    case string a when a.Contains("BRIDGE"):
                          footstepBridge.enabled   = true;
                          terrainFootPrev = footstepBridge;
                          break;

                    default:
                          break;

              }


              if(terrainFootPrev != terrainFootNext)
                {
                    terrainFootNext.enabled = false;
                    terrainFootNext = terrainFootPrev;
                }

            }else{
              footstepGrass.enabled = false;  // 4
              footstepFloor.enabled = false;
              footstepSand.enabled = false;
              footstepWater.enabled = false;
              footstepSnow.enabled = false;
              footstepBridge.enabled = false;
            }

  }

}
