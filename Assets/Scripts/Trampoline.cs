using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trampoline : MonoBehaviour
{
    private Animator animator;
    public float force;

    void Start(){
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == ("Player")) {
            animator.SetTrigger("Ativa");
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, force), ForceMode2D.Impulse);          
        }
    }   
}