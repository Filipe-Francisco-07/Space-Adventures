using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
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

    public GameObject projectilePrefab;
    public Transform firePoint; 
    private bool canShoot = true;
    public GameObject orb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        Andar();
        Pular();
        Atirar();

        Scene currentscene = SceneManager.GetActiveScene();
        if (currentscene.name == "CenaPreBoss" && GerenciadorDeJogo.instance.orbCollected)
        {
            orb.SetActive(true);
        }
    }

    void Andar()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * velocidade, rb.velocity.y);

        if (horizontal > 0f)
        {
            if (pulando)
            {
                animacao.SetBool("jump", true);
                animacao.SetBool("walk", false);
                transform.localScale = new Vector3(1f, 1f, 1f); // Vira para a direita
            }
            else
            {
                animacao.SetBool("walk", true);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else if (horizontal < 0f)
        {
            if (pulando)
            {
                animacao.SetBool("jump", true);
                animacao.SetBool("walk", false);
                transform.localScale = new Vector3(-1f, 1f, 1f); // Vira para a esquerda
            }
            else
            {
                animacao.SetBool("walk", true);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            if (pulando)
            {
                animacao.SetBool("jump", true);
                animacao.SetBool("walk", false);
            }
            else
            {
                animacao.SetBool("walk", false);
            }
        }
    }

    void Pular()
    {
        if (Input.GetButtonDown("Jump") && !winding)
        {
            if (!pulando)
            {
                rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                jumpDuplo = true;
                animacao.SetBool("jump", true);
                MusicPlayer.instance.PlaySound(MusicPlayer.instance.jump);
            }
            else
            {
                if (jumpDuplo)
                {
                    rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                    jumpDuplo = false;
                    MusicPlayer.instance.PlaySound(MusicPlayer.instance.jump);
                }
            }
        }
    }

    void Atirar()
    {
        Scene currentscene = SceneManager.GetActiveScene();
        if (((currentscene.name == "CenaBoss" || (currentscene.name == "CenaPreBoss" || currentscene.name == "CenaFinal") && GerenciadorDeJogo.instance.orbCollected)) && canShoot)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.F))
            {
                Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetDirection(direction);
                canShoot = false;
                StartCoroutine(ResetShoot());
            }
        }
    }

    private IEnumerator ResetShoot()
    {
        MusicPlayer.instance.PlaySound(MusicPlayer.instance.orbShoot);
        yield return new WaitForSeconds(0.25f);
        canShoot = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            pulando = false;
            animacao.SetBool("jump", false);
        }
        if (collision.gameObject.layer == 10 && GerenciadorDeJogo.instance.currentCoins >= 500
        )
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFase1.2");
        }
        if (collision.gameObject.layer == 11 && GerenciadorDeJogo.instance.currentCoins >= 500
        )
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFase1.3");
        }
        if (collision.gameObject.layer == 12 && GerenciadorDeJogo.instance.currentCoins >= 600
        )
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFase2");
        }
        if (collision.gameObject.layer == 13 && GerenciadorDeJogo.instance.currentCoins >= 500
        )
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFase2.2");
        }
        if (collision.gameObject.layer == 14 && GerenciadorDeJogo.instance.currentCoins >= 700
        )
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFase2.3");
        }
        if (collision.gameObject.layer == 17 && GerenciadorDeJogo.instance.orbCollected)
        {
            GerenciadorDeJogo.instance.ResetHealth();
            GerenciadorDeJogo.instance.TrocarCena("CenaBoss");
        }
        if (collision.gameObject.layer == 19 && GerenciadorDeJogo.instance.currentCoins >= 500
        )
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaPreBoss");
        }
         if (collision.gameObject.layer == 20)
        {
            GerenciadorDeJogo.instance.TrocarCena("CenaFinal");
            GerenciadorDeJogo.instance.irFinal.SetActive(false);
        }
    }

    void OnTriggerStay2D()
    {
        winding = true;
    }

    void OnTriggerExit2D()
    {
        winding = false;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            pulando = true;
        }
    }
}