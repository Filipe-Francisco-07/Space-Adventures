using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private bool entered;
    
    void Update(){
        if(entered && !GerenciadorDeJogo.instance.orbCollected){
            if(Input.GetKeyDown(KeyCode.E)){
                GerenciadorDeJogo.instance.openChest.SetActive(false);
                GerenciadorDeJogo.instance.orbReceive.SetActive(true);
                GerenciadorDeJogo.instance.CollectedOrb();
                entered = true;
            }
        }
    }
     void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !entered)
        {
            GerenciadorDeJogo.instance.openChest.SetActive(true);
            entered = true;

        }
    }

     void OnTriggerExit2D(Collider2D collider)
    {
        GerenciadorDeJogo.instance.openChest.SetActive(false);
        if(!GerenciadorDeJogo.instance.orbCollected){
            entered = false;
        }
    }
}
