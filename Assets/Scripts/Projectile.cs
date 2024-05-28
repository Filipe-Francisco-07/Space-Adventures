using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // Velocidade do projétil
    private Vector2 direction;

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        Destroy(gameObject, 2f); // Destroi o projétil após 2 segundos
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GerenciadorDeJogo.instance.BossHit();
        Destroy(gameObject);
    }
}