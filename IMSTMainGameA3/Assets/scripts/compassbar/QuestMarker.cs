// QuestMarker.cs
using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour, IQuestMarker
{
    public Sprite icon;
    public Image image;
    public mondmaskerdispenser praatpaal;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void RemoveMarker()
    {
        if (image != null)
        {
            image.enabled = false;
        }
    }

    public void SetImage(Image newImage)
    {
        image = newImage;
    }

    void Update()
    {
        if (praatpaal.hasinteracted == true)
        {
            RemoveMarker();
        }
    }
}


