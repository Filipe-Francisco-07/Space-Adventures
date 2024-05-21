using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoqueScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("Player")) {
            Scene currentscene = SceneManager.GetActiveScene();
            GerenciadorDeJogo.instance.KillPlayer(collider,currentscene.name.ToString());
        }   
    }
}
