using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private bool entered;
    public GameObject chest;
    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }
    
    void Update(){
        if(entered && !GerenciadorDeJogo.instance.orbCollected){
            if(Input.GetKeyDown(KeyCode.E)){
                MusicPlayer.instance.PlaySound(MusicPlayer.instance.OpenChest);
                anim.SetBool("IsOpened",true);
                GerenciadorDeJogo.instance.openChest.SetActive(false);
                GerenciadorDeJogo.instance.orbReceive.SetActive(true);
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
