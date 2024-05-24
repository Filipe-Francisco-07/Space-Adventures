using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    private bool isDying;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("Player") && !isDying) {
            isDying = true;
            Scene currentScene = SceneManager.GetActiveScene();
            GerenciadorDeJogo.instance.KillPlayer(collider, currentScene.name);
            StartCoroutine(ResetarMorte(0.5f));
        }   
    }

    IEnumerator ResetarMorte(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDying = false;
    }
}
