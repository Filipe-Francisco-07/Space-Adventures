using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform right;
    public Transform left;
    public Transform top;
    private bool colisao;
    [SerializeField] float velocidade;
    public LayerMask lay;
    private bool morreu;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimenta o inimigo na direção atual
        rb.velocity = new Vector2(velocidade, rb.velocity.y);

        // Detecta a colisão com a parede usando Raycast
        RaycastHit2D hit = Physics2D.Raycast(right.position, Vector2.right, 0.1f, lay);
        if (hit.collider != null)
        {
            InverterDirecao();
        }

        hit = Physics2D.Raycast(left.position, Vector2.left, 0.1f, lay);
        if (hit.collider != null)
        {
            InverterDirecao();
        }
    }

    // Inverte a direção do movimento
    void InverterDirecao()
    {
        // Inverte a escala no eixo X para mudar a direção visualmente
        transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);

        // Inverte a velocidade para mudar a direção do movimento
        velocidade = -velocidade;
    }


    void OnCollisionEnter2D(Collision2D colisao){
        if(colisao.gameObject.tag == "Player"){
            float altura = colisao.contacts[0].point.y - top.position.y;
            if(altura > 0 && !morreu){
                colisao.gameObject.GetComponent<Rigidbody2D>().AddForce( Vector2.up * 10, ForceMode2D.Impulse);  
                velocidade = 0;
                Destroy(gameObject,0.25f);
            }else{
                morreu = true;
                Scene currentscene = SceneManager.GetActiveScene();
                GerenciadorDeJogo.instance.KillPlayer(colisao.collider,currentscene.name.ToString());
            }
        }
    }
}
