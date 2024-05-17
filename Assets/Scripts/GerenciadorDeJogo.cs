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
    public void UpdateCoins()
    {
       showCoins.text = totalCoins.ToString();
    }

    public void ZerarCoins(){
        totalCoins = 0;
    }
   public void TrocarCena(string nomeCena)
    {    
        SceneManager.LoadScene(nomeCena);
    }
}
