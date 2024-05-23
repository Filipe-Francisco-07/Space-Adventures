using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCoin : MonoBehaviour
{

    private SpriteRenderer sr;
    private CircleCollider2D cc;
    public GameObject collect;
    public int numColeta;
    private bool block;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        block = false;
    }

   void OnTriggerEnter2D(Collider2D collider){
        if(!block){
            if(collider.gameObject.CompareTag("Player")){
                sr.enabled = false;
                cc.enabled = false;
                collect.SetActive(true);
                GerenciadorDeJogo.instance.collectedCoins += numColeta;
                GerenciadorDeJogo.instance.UpdateCoins();
                block = true;
                Destroy(gameObject, 0.75f);
            }
        }else{
            Espera(0.3f);
            block = false;
        }
    }   

     IEnumerator Espera(float num)
    {
        yield return new WaitForSeconds(num);
    }
 
}
