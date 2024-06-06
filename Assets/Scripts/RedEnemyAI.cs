using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedEnemyAI : MonoBehaviour{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform right;
    public Transform left;
    public Transform top;
    private bool colisao;
    [SerializeField] float velocidade;
    public LayerMask lay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {  
        rb.velocity = new Vector2(velocidade, rb.velocity.y);

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

    void InverterDirecao()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
        velocidade = -velocidade;
    }


    void OnCollisionEnter2D(Collision2D colisao){
        if(colisao.gameObject.tag == "Player"){
                Scene currentscene = SceneManager.GetActiveScene();
                GerenciadorDeJogo.instance.KillPlayer(colisao.collider,currentscene.name.ToString());
        }
    }
}
