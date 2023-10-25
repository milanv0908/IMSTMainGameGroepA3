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

    private void Start() {
        compassUnit = compassImage.rectTreansform.rect.width / 360f; 
    }

    private void Update() {
        compassImage.uvRect = new Rect (player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
    public void AddQuestMarker (QuestMarker marker) {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<compassImage>();
        marker.image.sprite = marker.icon;
        questMarkers.Add(marker); 
    }

    Vector2 GetPosOnCompass (QuestMarker marker) {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.Z)
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.Z)

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f)
    }
}
