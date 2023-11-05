// QuestMarker.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour, IQuestMarker
{
    public Sprite icon;
    public Image image;
    public QuestMarkerNPC jonas;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

        void Start() {

        StartCoroutine(uit());

    }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void AddMarker()
    {
        if (image != null)
        {
            jonas.jonasinteracted = false;
            image.enabled = true;
            Debug.Log("enable");
        }
    }

    public void RemoveMarker() {
        image.enabled = false;
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (jonas.jonasinteracted == true)
        {
            AddMarker();
            Debug.Log("jonasinteracted");
        }
    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}
}


