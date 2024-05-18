using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeJogo : MonoBehaviour
{
    public int totalCoins;
    public Text showCoins;
    public static GerenciadorDeJogo instance;

    private void Start()
    {
        instance = this;

    }

    private void Awake()
    {
        /*if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }*/

        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
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
