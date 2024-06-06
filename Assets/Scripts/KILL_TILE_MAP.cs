using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KILL_TILE_MAP : MonoBehaviour
{
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            Scene currentscene = SceneManager.GetActiveScene();
            GerenciadorDeJogo.instance.KillPlayer(collider,currentscene.name.ToString());
    }
}
}   
