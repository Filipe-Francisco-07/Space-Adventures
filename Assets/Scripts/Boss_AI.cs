using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public Transform player;  // Referência ao jogador
    public float speed = 2f;
    public float attackDistance = 2f;  // Distância para iniciar o ataque
    private bool isAttacking = false;
    private bool facingRight = true;  // Controle de direção
    private PolygonCollider2D bossCollider; // Referência ao colisor do boss

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<PolygonCollider2D>(); // Obtém a referência ao colisor do boss
    }

    void Update()
    {
        // Verifica a distância entre o boss e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Se estiver atacando, não se move
        if (isAttacking)
        {
            animator.SetBool("Attacking", true);
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", false);
            return;  // Sai do Update para evitar movimento enquanto ataca
        }

        // Se estiver dentro da distância de ataque, ataca
        if (distanceToPlayer <= attackDistance)
        {
            StartCoroutine(Attack());
        }
        else
        {
            // Se estiver fora da distância de ataque, anda em direção ao jogador
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Attacking", false);

            // Move em direção ao jogador
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            // Vira o boss para a direção correta
            if (direction.x > 0 && facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && !facingRight)
            {
                Flip();
            }
        }

        // Atualiza a posição do colisor do boss para coincidir com a posição do boss
        bossCollider.transform.position = transform.position;
    }

    IEnumerator Attack()
    {
        Debug.Log("Atacando");
        isAttacking = true;
        animator.SetBool("Attacking", true);
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", false);
        rb.velocity = Vector2.zero;  // Para o movimento durante o ataque
        
        // Duração do ataque
        yield return new WaitForSeconds(1f);

        isAttacking = false;
        animator.SetBool("Attacking", false);
    }

    // Método para virar o boss
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}