using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    private static bool isDying;

    void Start()
    {
        // Reseta isDying para false quando a cena é carregada
        isDying = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !isDying)
        {
            isDying = true;
            Scene currentScene = SceneManager.GetActiveScene();
            GerenciadorDeJogo.instance.KillPlayer(collider, currentScene.name);
            StartCoroutine(ResetarMorte(0.7f));
        }
    }

    IEnumerator ResetarMorte(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDying = false;
    }
}