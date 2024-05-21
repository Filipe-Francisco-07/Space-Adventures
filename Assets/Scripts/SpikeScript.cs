using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    private bool block = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(!block){
            if (collider.gameObject.CompareTag("Player")) {
                Scene currentscene = SceneManager.GetActiveScene();
                GerenciadorDeJogo.instance.KillPlayer(collider,currentscene.name.ToString());
                block = true;
            }   
        }else{
            Espera();
        }
    }
    IEnumerator Espera()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
