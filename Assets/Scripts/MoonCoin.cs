using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCoin : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    public GameObject collect;
    public int numColeta;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            MusicPlayer.instance.PlaySound(MusicPlayer.instance.coin);
            sr.enabled = false;
            cc.enabled = false;
            collect.SetActive(true);
            GerenciadorDeJogo.instance.collectedCoins += numColeta;
            GerenciadorDeJogo.instance.UpdateCoins();
            Destroy(gameObject, 0.75f);
        }
    }
}
