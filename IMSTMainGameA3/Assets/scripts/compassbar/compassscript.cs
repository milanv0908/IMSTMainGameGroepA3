using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compassscript : MonoBehaviour
{
    public GameObject iconPrefab;
    List<QuestMarker> questMarkers = new List<QuestMarker>();
    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    public QuestMarker Frank;

    public bus buss;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        AddQuestMarker(Frank);
        iconPrefab.SetActive(true);
    }

private void Update()
{
    compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

    foreach (QuestMarker marker in questMarkers)
    {
        marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
    }

    if (buss.playerd == true)
    {
        compassImage.enabled = false; // Disabling the RawImage component
        if (iconPrefab.activeSelf)
        {
            iconPrefab.SetActive(false); // Disabling the entire iconPrefab GameObject
            Debug.Log("iconPrefab is now set to inactive");
        }
    }
    else
    {
        compassImage.enabled = true;
        if (!iconPrefab.activeSelf)
        {
            iconPrefab.SetActive(true); // Enabling the entire iconPrefab GameObject
            Debug.Log("iconPrefab is now set to active");
        }
    }
}

    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;
        questMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z); // Changed 'Z' to lowercase 'z'
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z); // Changed 'Z' to lowercase 'z'

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }

 
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class compassscript : MonoBehaviour
// {
//     public GameObject iconPrefab;
//     List<QuestMarker> questMarkers = new List<QuestMarker>();
//     public RawImage compassImage;
//     public Transform player;

//     float compassUnit;

//     public QuestMarker Frank;

//     private void Start() {
//         compassUnit = compassImage.rectTransform.rect.width / 360f;
//         AddQuestMarker(Frank);
//     }

//     private void Update() {
//         compassImage.uvRect = new Rect (player.localEulerAngles.y / 360f, 0f, 1f, 1f);

//         foreach (QuestMarker marker in questMarkers) {
//             marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

//         }
//     }
//     public void AddQuestMarker (QuestMarker marker) {
//         GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
//         marker.image = newMarker.GetComponent<compassImage>();
//         marker.image.sprite = marker.icon;
//         questMarkers.Add(marker); 
//     }

//     Vector2 GetPosOnCompass (QuestMarker marker) {
//         Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.Z);
//         Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.Z);

//         float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

//         return new Vector2(compassUnit * angle, 0f);
//     }
// }
