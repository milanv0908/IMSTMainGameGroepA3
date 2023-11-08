using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questmarkersuzanne : MonoBehaviour, IQuestMarker
{

     public PeterInteract peterInteractScript;
     public TateInteract tateInteractScript;
     public GertInteract gertInteractScript;

     public bool heeftshits = false;


    public Sprite icon;
    public Image image;
    public streetevent Streetevent;

    public Sprite Icon { get { return icon; } }
    public Image Image { get { return image; } }

     private Vector3 originalPosition; // Variabele om de oorspronkelijke positie op te slaan

        void Start() {

             originalPosition = transform.position;
        // Stel de Z-positie in op -1000
        transform.position = new Vector3(transform.position.x, transform.position.y, -1000);

        StartCoroutine(uit());

    }

    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }

    public void AddMarker()
    {
        if (image != null )
        {
            
            image.enabled = true;
            Debug.Log("enable");
        }
    }

    public void hasinteracted(){
        RemoveMarker();
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
        if (peterInteractScript.PeterTalked == true && tateInteractScript.TateTalked == true && gertInteractScript.GertTalked == true && heeftshits == false)
        {
              transform.position = originalPosition;
            AddMarker();
            Debug.Log("huts");
            heeftshits = true;
        }

    }

                      IEnumerator uit() {
        yield return new WaitForSeconds(0.1f);
        image.enabled = false;
    

}
}
