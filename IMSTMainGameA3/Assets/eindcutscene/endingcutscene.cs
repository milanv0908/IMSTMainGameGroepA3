using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingcutscene : MonoBehaviour
{
    public frank Frank;
    Animator animator;
    
    void Start()
    {
    animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Frank.beginend == true)
        animator.SetTrigger("start");
        StartCoroutine(end());
    }

        IEnumerator end() {
        yield return new WaitForSeconds(13);
        SceneManager.LoadScene("start menu", LoadSceneMode.Single);

}
}
