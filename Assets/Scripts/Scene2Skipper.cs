using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2Skipper : MonoBehaviour
{
    void Start()
    {
        // Inicia a rotina de troca de cena
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(2.3f);

        SceneManager.LoadScene("CenaInicial");
    }
}