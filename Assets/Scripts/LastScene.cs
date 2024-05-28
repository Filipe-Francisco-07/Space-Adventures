using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LastScene : MonoBehaviour
{
    public TMP_Text ShowMessage;
    
    void Start()
    {
        Atualizar();

    }

    void Atualizar(){
        string frase = "CONGRATULATIONS "+PlayerPrefs.GetString("playerName")+", YOU'VE BEATED THE GAME AND RECOVERED LUNARIS07, NOW YOU ARE WITH "+PlayerPrefs.GetInt("totalCoins")+" POINTS, THAT'S AWESOME!";

        ShowMessage.text = frase;
    }
    public void Restart(){
        GerenciadorDeJogo.instance.ResetHealth();
        GerenciadorDeJogo.instance.TrocarCena("CenaFase1");
    }

    public void MainMenu(){
        GerenciadorDeJogo.instance.TrocarCena("CenaInicial");
    }

    public void ShowStats(){
        
    }

    public void ExitGame(){
        Application.Quit();
    }

}
