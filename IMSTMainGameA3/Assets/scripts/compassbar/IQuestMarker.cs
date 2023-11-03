// IQuestMarker.cs
using UnityEngine;
using UnityEngine.UI;

public interface IQuestMarker
{
    Sprite Icon { get; }
    Image Image { get; } // This should remain as a getter
    Vector2 Position { get; }
    void RemoveMarker();
    void SetImage(Image image); // Add this setter method
}