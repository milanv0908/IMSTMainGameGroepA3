using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nieuwescene : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        StartCoroutine(nextscene());
        animator = GetComponent<Animator>(); 
    }

    IEnumerator nextscene()
    {
        yield return new WaitForSeconds(60);
        animator.SetTrigger("open");
    }
}
