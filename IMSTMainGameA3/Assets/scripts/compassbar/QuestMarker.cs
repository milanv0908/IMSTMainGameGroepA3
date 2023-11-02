using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour
{
    public Sprite icon;
    public Image image;

    public Vector2 position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    // Function to remove the quest marker
    public void RemoveMarker()
    {
        // Deactivate the Image component to hide the marker
        if (image != null)
        {
            image.enabled = false;
        }
    }

    // You can call this method when the player interacts with the object
    public void InteractWithObject()
    {
        // Perform the interaction logic here

        // Then remove the quest marker
        RemoveMarker();
    }
}
