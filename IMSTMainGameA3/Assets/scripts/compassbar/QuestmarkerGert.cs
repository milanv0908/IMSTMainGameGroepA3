using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestmarkerGert : MonoBehaviour, IQuestMarker
{

    public Sprite icon;
    public Image image;
    public streetevent Streetevent;

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
        if (Streetevent.hasinteracted == true)
        {
            AddMarker();
            Debug.Log("huts");
        }
    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}

}
