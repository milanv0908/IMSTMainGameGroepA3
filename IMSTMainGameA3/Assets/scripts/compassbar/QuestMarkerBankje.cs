// QuestMarkerBankje.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarkerBankje : MonoBehaviour, IQuestMarker
{
    public Sprite icon;
    public Image image;
    public mondmaskerdispenser mondmask;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

    
    void Start() {

            
            Debug.Log("hoi");
        StartCoroutine(uit());

    }

                  IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void RemoveMarker()
    {
      
            image.enabled = true;
            
        
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (mondmask.hasinteracted == true)
        {
            RemoveMarker();
        }
    }
}


