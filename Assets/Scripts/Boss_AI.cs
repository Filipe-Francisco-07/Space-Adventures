using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_AI : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public Transform player; 
    public float speed = 2f;
    public float attackDistance = 2f;  
    private bool isAttacking = false;
    private bool facingRight = true; 
    private PolygonCollider2D bossCollider;
    private bool isDying;
    public GameObject sword;  
    private Animator swordAnimator;  

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        swordAnimator = sword.GetComponent<Animator>();
        isDying = false;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
            
            if (player == null)
            {
                return;
            }
        }

        if (GerenciadorDeJogo.instance.bossHealth <= 0)
        {
            Destroy(gameObject, 0.5f);
            GerenciadorDeJogo.instance.BossKiller();
            StartCoroutine(Esperar());
            GerenciadorDeJogo.instance.TrocarCena("CenaFinal");
            return;
        }

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

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(5f);
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