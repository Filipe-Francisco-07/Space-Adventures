using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    private static bool isDying;

    void Start()
    {
        isDying = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !isDying)
        {
            isDying = true;
            Scene currentScene = SceneManager.GetActiveScene();
            StartCoroutine(ResetarMorte(0.5f));
            GerenciadorDeJogo.instance.KillPlayer(collider, currentScene.name);
        }
    }
    IEnumerator ResetarMorte(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}