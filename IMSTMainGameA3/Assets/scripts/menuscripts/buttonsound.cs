// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class buttonsound : MonoBehaviour
// {
//     public AudioSource audioSource;
//     public AudioClip hover;
//     public AudioClip click;

//     public void HoverSound() {
//         audioSource.PlayOneShot(hover);
//         Debug.Log("hover");
//     }

//     public void ClickSound() {
//         audioSource.PlayOneShot(click);
//         Debug.Log("click");
//     }


// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hover;
    public AudioClip click;

    private bool isPlayingClick = false;

    public void HoverSound()
    {
        if (!isPlayingClick)
        {
            audioSource.PlayOneShot(hover);
            Debug.Log("hover");
        }
    }

    public void ClickSound()
    {
        if (!isPlayingClick)
        {
            StartCoroutine(PlayClickSound());
        }
    }

    private IEnumerator PlayClickSound()
    {
        isPlayingClick = true;
        audioSource.PlayOneShot(click);
        yield return new WaitForSeconds(click.length);
        isPlayingClick = false;
    }
}


