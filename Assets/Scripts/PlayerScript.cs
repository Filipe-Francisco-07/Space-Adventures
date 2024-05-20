using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour{

    public int coins;
    public int healthPoints;
    public bool pulando = false;
    public bool jumpDuplo;
    private Rigidbody2D rb;
    [SerializeField] int jump = 10;
    private float horizontal;
    private float vertical;
    [SerializeField] int velocidade = 5;
    private Animator animacao;
    private bool winding;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        Andar();
        Pular();
    }

    void Andar() {
        horizontal = Input.GetAxis("Horizontal");
        // transform.Translate(new Vector3(horizontal, 0, 0) * Time.deltaTime * velocidade);
        rb.velocity = new Vector2(horizontal * velocidade, rb.velocity.y);

        if (horizontal > 0f) {
            if(pulando){
                animacao.SetBool("jump", true);
                animacao.SetBool("walk", false);
            }else{
                animacao.SetBool("walk", true);
                transform.localScale = new Vector3(1f, 1f, 1f); 
            }
            }else if (horizontal < 0f) {
                if(pulando){
                    animacao.SetBool("jump", true);
                    animacao.SetBool("walk", false);
                }else{
                    animacao.SetBool("walk", true);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            } else {
                if(pulando){
                    animacao.SetBool("jump", true);
                    animacao.SetBool("walk", false);
                }else{
                    animacao.SetBool("walk", false);
                }
            }
        }

    void Pular(){

        if(Input.GetButtonDown("Jump") && !winding){
            if(!pulando){
                rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                jumpDuplo = true;
                animacao.SetBool("jump", true);
                
            } else{
                if(jumpDuplo){
                    rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                    jumpDuplo = false;
                
                }
            }

        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 8)
        {
            pulando = false;
             animacao.SetBool("jump", false);
        }
        if(collision.gameObject.layer == 10 )
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFase1.2");
        }
         if(collision.gameObject.layer == 11)
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFase1.3");
        }
          if(collision.gameObject.layer == 13)
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaMain2");
        }
         if(collision.gameObject.layer == 14)
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaBoss");
        }
       
    }

    void OnTriggerStay2D(){
        winding = true;
    }

    void OnTriggerExit2D(){
        winding = false;
    }

     void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 8)
        {
            pulando = true;
        }
    }
}


