using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogar : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("CenaFase1");
           GerenciadorDeJogo.instance.ZerarCoins();
    }
}
