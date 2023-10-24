using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compassscript : MonoBehaviour
{
    public RawImage compassImage;
    public Transform player;

    private void update() {
        compassImage.uvRect = new Rect (player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
}
