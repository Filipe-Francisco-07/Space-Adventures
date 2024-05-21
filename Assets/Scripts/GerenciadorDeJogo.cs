using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeJogo : MonoBehaviour
{
    public int totalCoins;
    public TMP_Text showCoins;
    public static GerenciadorDeJogo instance;
    public GameObject HealthPoints;
    private int currentHealth;
    private Image[] heartImages;

    void Start()
    {
        instance = this;
        heartImages = HealthPoints.GetComponentsInChildren<Image>();
        currentHealth = heartImages.Length; 
        Debug.Log(currentHealth);

    }

    private void Awake()
    {
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
        if(currentHealth > 0){
            currentHealth --;
            heartImages[currentHealth].enabled = false;
            Destroy(player.gameObject);
            if(currentHealth == 0){
                TrocarCena("CenaFase1");   
                ResetHealth(); 
            }else{
                TrocarCena(nome); 
            }
        }
        Debug.Log(currentHealth);
    }

    public void ResetHealth()
{
    currentHealth = heartImages.Length;
    foreach (var heart in heartImages)
    {
        heart.enabled = true;
    }
}
   public void TrocarCena(string nomeCena)
    {    
        SceneManager.LoadScene(nomeCena);
    }
}
