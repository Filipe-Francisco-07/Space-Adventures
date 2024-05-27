using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitsPlayer : MonoBehaviour
{
    public GameObject sword;
    private Animator swordAnimator;
    
    void Start(){
        swordAnimator = sword.GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D colisao){
        if(colisao.gameObject.tag == "Player" && swordAnimator.GetBool("Attacking") ){ 
            Scene currentscene = SceneManager.GetActiveScene();
            GerenciadorDeJogo.instance.KillPlayer(colisao.collider,currentscene.name.ToString());
        }
    }

}
