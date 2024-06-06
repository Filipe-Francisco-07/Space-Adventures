using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Skipper : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(2.3f);

        SceneManager.LoadScene("CenaPreInicial");
    }
}