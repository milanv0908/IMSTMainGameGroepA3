using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TV : MonoBehaviour
{
    public bool image1off = false;
    public bool image2off = false;
    public bool haspressed = false;
    public bool playadd = false;
    public bool leave = false;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public RawImage add;
    public VideoPlayer video;
    public voice Voice;

    void Start()
    {
        image1.enabled = true;
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = false;
        add.enabled = false;

        // Ensure that the VideoPlayer is set to not play on awake
        video.playOnAwake = false;
    }

    void Update()
    {
        if (image1off == true && haspressed == false)
        {
            image1.enabled = false;
            image2.enabled = true;
            image3.enabled = false;
            image4.enabled = false;
            image1off = false;
            Voice.buttonpress = true;
            haspressed = true;
            StartCoroutine(nextpage());
        }
    }

    public void knoppie()
    {
        if (!haspressed)
        {
            image1off = true;
        }
    }

    IEnumerator nextpage()
    {
        yield return new WaitForSeconds(10);
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = false;
        add.enabled = true;
        video.Play();
        StartCoroutine(endadd());

    }

    IEnumerator endadd() {
        yield return new WaitForSeconds(10);
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = true;
        image4.enabled = false;
        add.enabled = false;
        Voice.diagnosis = true;
        video.Pause();
        StartCoroutine(ending());
    }

    IEnumerator ending() {
        yield return new WaitForSeconds(5);
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = true;
        Voice.headsetoff = true;
        add.enabled = false;
        leave = true;
        Debug.Log("hutsa");
    }
}