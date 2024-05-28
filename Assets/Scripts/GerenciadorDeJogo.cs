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
    private int totalCollected;
    public int bossHealth = 1000;
    private int currentBossHP; 
    public Slider healthSlider;
    public TMP_Text showBossHP;
    public GameObject objectToDestroy;

    void Start()
    {
        totalCoins = 0;
        instance = this;
        heartImages = HealthPoints.GetComponentsInChildren<Image>();
        currentHealth = heartImages.Length; 
        block = false;
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == "CenaBoss"){
            
            healthSlider.gameObject.SetActive(true);
            currentBossHP = bossHealth;
            healthSlider.maxValue = 1000;
            healthSlider.value = currentBossHP;
            showBossHP.text = bossHealth.ToString();
        }

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

     public void BossKiller()
    {
        collectedCoins += 5000;
        UpdateCoins();
        SaveData();
    }

    public void ResetLevelCoins()
    {
        totalCoins -= currentCoins;
        currentCoins = 0;
        showCoins.text = totalCoins.ToString();
    }

    public void ZerarCoins(){
        collectedCoins = 0;
        totalCoins = 0;
        showCoins.text = totalCoins.ToString();
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
}

    public void BossHit(){
        bossHealth -= 30;
        if(bossHealth <= 0){
            healthSlider.value = 0;
            showBossHP.text = 0.ToString();
        }else{
        healthSlider.value = bossHealth;
        showBossHP.text = bossHealth.ToString();
        }
    }
   public void TrocarCena(string nomeCena)
    {
        block = false;
        SaveData();
        currentCoins = 0;
        SceneManager.LoadScene(nomeCena);

    }

    private void SaveData()
    {
        totalCollected += currentCoins;
        PlayerPrefs.SetInt("totalCoins", totalCollected);
        PlayerPrefs.Save(); 
    }

    private void LoadData()
    {
        totalCollected = PlayerPrefs.GetInt("totalCoins");
        showCoins.text = totalCoins.ToString();
    }

}
