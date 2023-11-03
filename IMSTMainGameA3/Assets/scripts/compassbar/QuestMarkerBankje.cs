// QuestMarkerBankje.cs
using UnityEngine;
using UnityEngine.UI;

public class QuestMarkerBankje : MonoBehaviour, IQuestMarker
{
    public Sprite icon;
    public Image image;
    public busstation bank;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    void Start() {
        if (image != null)
        {
            image.enabled = false;
        }
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
        if (bank.playanimation == true)
        {
            RemoveMarker();
        }
    }
}


