using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GerenciadorDeJogo : MonoBehaviour
{
    public int totalCoins;
    public TMP_Text showCoins;
    public static GerenciadorDeJogo instance;

    void Start()
    {
        instance = this;

    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void UpdateCoins()
    {
        if (showCoins !=null)
       showCoins.text = totalCoins.ToString();
    }

    public void ZerarCoins(){
        totalCoins = 0;
    }
    public void KillPlayer(Collider2D player,string nome){
        Destroy(player.gameObject);
        TrocarCena(nome);
    }
   public void TrocarCena(string nomeCena)
    {    
        SceneManager.LoadScene(nomeCena);
    }
}
