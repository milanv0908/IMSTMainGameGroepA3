using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nieuwescene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(nextscene());
    }

    IEnumerator nextscene()
    {
        yield return new WaitForSeconds(60);
        SceneManager.LoadScene("Expansionscene2");
    }
}
