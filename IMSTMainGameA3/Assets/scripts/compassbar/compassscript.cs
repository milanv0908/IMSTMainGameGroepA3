// compassscript.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compassscript : MonoBehaviour
{
    public GameObject iconPrefab;
    List<IQuestMarker> questMarkers = new List<IQuestMarker>();
    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    public QuestMarker Frank;
    public QuestMarkerBankje bankje;
    public QuestMarkerNPC Jonas;
    public QuestmarkerGert Gert;

    public bus buss;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        AddQuestMarker(Frank);
        AddQuestMarker(bankje);
        AddQuestMarker(Jonas);
        AddQuestMarker(Gert);
        iconPrefab.SetActive(true);
    }

    private void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach (IQuestMarker marker in questMarkers)
        {
            marker.Image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }

        // ...
    }

    public void AddQuestMarker(IQuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.SetImage(newMarker.GetComponent<Image>());
        marker.Image.sprite = marker.Icon;
        questMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(IQuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.Position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }

    // ...
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
