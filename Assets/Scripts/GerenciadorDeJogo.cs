using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeJogo : MonoBehaviour
{
    public int totalCoins;
    public int collectedCoins;
    public int currentCoins;
    public TMP_Text showCoins;
    public static GerenciadorDeJogo instance;
    public GameObject HealthPoints;
    private int currentHealth;
    private Image[] heartImages;
    private bool block;
    private string playerName = "Filipe";
    private string totalCoinsKey = "TotalCoins";
    private string playerNameKey = "PlayerName";

    void Start()
    {
        instance = this;
        heartImages = HealthPoints.GetComponentsInChildren<Image>();
        currentHealth = heartImages.Length; 
        block = false;

        LoadData();
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
        totalCoins += collectedCoins;
        currentCoins += collectedCoins;
        showCoins.text = totalCoins.ToString();
        collectedCoins = 0;
    }

    public void ResetLevelCoins()
    {
        totalCoins -= currentCoins;
        currentCoins = 0;
        showCoins.text = totalCoins.ToString();

        SaveData();
    }

    public void ZerarCoins(){
        collectedCoins = 0;
        totalCoins = 0;
        showCoins.text = totalCoins.ToString();

        SaveData();
    }
    public void KillPlayer(Collider2D player,string nome){
        if(!block){
            if(currentHealth > 0){
                currentHealth --;
                heartImages[currentHealth].enabled = false;
                Destroy(player.gameObject);
                block = true;
                ResetLevelCoins();
                if(currentHealth == 0){
                    TrocarCena("CenaFase1");   
                    ResetHealth(); 
                    ZerarCoins();
                }else{
                    TrocarCena(nome); 
                }
        }
        } else{
            block = false;
            Espera(0.3f);
        }
        SaveData();
    }

    IEnumerator Espera(float num)
    {
        yield return new WaitForSeconds(num);
    }

    public void ResetHealth()
{
    currentHealth = heartImages.Length;
    foreach (var heart in heartImages)
    {
        heart.enabled = true;
    }
     SaveData();
}
   public void TrocarCena(string nomeCena)
    {
        block = false;
        currentCoins = 0;
        SceneManager.LoadScene(nomeCena);

        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(totalCoinsKey, totalCoins);
        PlayerPrefs.SetString(playerNameKey, playerName);
        PlayerPrefs.Save(); // Salva os dados no disco imediatamente
    }

    // Método para carregar os dados salvos
    private void LoadData()
    {
        totalCoins = PlayerPrefs.GetInt(totalCoinsKey, 0);
        playerName = PlayerPrefs.GetString(playerNameKey, "Filipe");
        showCoins.text = totalCoins.ToString();
    }

}
