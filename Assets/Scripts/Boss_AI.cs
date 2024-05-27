using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    private PolygonCollider2D bossCollider;
    private bool isDying;
    public GameObject sword;  // Referência ao GameObject da espada
    private Animator swordAnimator;  // Referência ao Animator da espada

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        swordAnimator = sword.GetComponent<Animator>();
        isDying = false;
    }

    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (isAttacking)
        {
            animator.SetBool("Attacking", true);
            swordAnimator.SetBool("Attacking", true);
            animator.SetBool("Walking", false);
            swordAnimator.SetBool("Walking", false);
            animator.SetBool("Idle", false);
            swordAnimator.SetBool("Idle", false);
            return;  
        }
        if (distanceToPlayer <= attackDistance)
        {
            StartCoroutine(Attack());
        }
        else
        {

            animator.SetBool("Walking", true);
            swordAnimator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
            swordAnimator.SetBool("Idle", false);
            animator.SetBool("Attacking", false);
            swordAnimator.SetBool("Attacking", false);

            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            if (direction.x > 0 && facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && !facingRight)
            {
                Flip();
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("Attacking", true);
        swordAnimator.SetBool("Attacking", true);
        animator.SetBool("Walking", false);
        swordAnimator.SetBool("Walking", false);
        animator.SetBool("Idle", false);
        swordAnimator.SetBool("Idle", false);
        rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(1f);

        isAttacking = false;
        animator.SetBool("Attacking", false);
        swordAnimator.SetBool("Attacking", false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !isDying && isAttacking)
        {
            isDying = true;
            Scene currentScene = SceneManager.GetActiveScene();
            GerenciadorDeJogo.instance.KillPlayer(collider, currentScene.name);
            StartCoroutine(ResetarMorte(0.7f));
        }
    }

    IEnumerator ResetarMorte(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDying = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
