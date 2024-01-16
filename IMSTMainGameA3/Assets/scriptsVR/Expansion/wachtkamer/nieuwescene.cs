using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nieuwescene : MonoBehaviour
{
    Animator animator;
    AudioSource audiosource;
    public AudioClip deuropen;
    public bool newscene = false;
    void Start()
    {
        StartCoroutine(nextscene());
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>(); 
    }

    IEnumerator nextscene()
    {
        yield return new WaitForSeconds(60);
        animator.SetTrigger("open");
        audiosource.PlayOneShot(deuropen);
        newscene = true;
    }
}
