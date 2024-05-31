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
    private bool paused;
    public GameObject pause;
    public GameObject lastScene;
    public GameObject GameInterface;
    public GameObject BossLifebar;
    public TMP_Text ShowLastMessage;
    public GameObject playerStats;
    public TMP_Text NameStats;
    public TMP_Text CoinStats;
    public bool lookingStats;
    public GameObject openChest;
    public GameObject orbReceive;
    public bool orbCollected;
    public bool isGameScene;
    private string currentSceneName = "";
    private bool playerDied = false;
    void Start()
    {
        orbCollected = false;
        lookingStats= false;
        instance = this;
        block = false;
        paused = false;
        LoadData();
    }
    void Update(){
        Scene currentScene = SceneManager.GetActiveScene();

        isGameScene = currentScene.name.StartsWith("CenaFase") || 
                           currentScene.name == "CenaPreBoss" || 
                           currentScene.name == "CenaBoss";

        if(!paused && isGameScene){
            if(Input.GetKeyDown(KeyCode.Escape)){
                Pause();
            }
        }else{
            if(Input.GetKeyDown(KeyCode.Escape) && !lookingStats){
                Resume();
            } else{
                HideStats();
            }
        }
    }

    public void CollectedOrb()
    {
        orbCollected = true;
        orbReceive.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        BossLifebar.SetActive(false);
        paused = true; 
    }

    public void Resume()
    {
        pause.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "CenaBoss"){
            BossLifebar.SetActive(true);
        }
        Time.timeScale = 1;   
        paused = false;
    }

     public void Restart()
     {
        currentHealth --;
        Scene currentScene = SceneManager.GetActiveScene();
        TrocarCena(currentScene.name);
        pause.SetActive(false);
        Time.timeScale = 1;   
        paused = false;
    }

    public void TotalRestart()
    {
        SaveData();
        ZerarCoins();
        ResetHealth();
        TrocarCena("CenaFase1");
        GameInterface.SetActive(true);
        BossLifebar.SetActive(false);
        lastScene.SetActive(false);
    }

    public void MainMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        GameInterface.SetActive(false);
        BossLifebar.SetActive(false);
        lastScene.SetActive(false);
        SaveData();
        ZerarCoins();
        ResetHealth();
        if (currentScene.name != "CenaFinal")
        {
           Resume();
        }
        TrocarCena("CenaInicial");
    }

        public void EndStats()
    {
        lastScene.SetActive(false);
        ShowStats();
    }
    public void ShowStats()
    {
        Resume();
        Time.timeScale = 0;
        playerStats.SetActive(true);
        NameStats.text = PlayerPrefs.GetString("playerName");
        CoinStats.text = PlayerPrefs.GetInt("totalCoins").ToString();
        lookingStats = true;
    }
    public void HideStats()
    {
        Time.timeScale = 1;
        playerStats.SetActive(false);
        lookingStats = false;
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "CenaFinal"){
            lastScene.SetActive(true);
        } else if(isGameScene){
            Pause();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
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
        MusicPlayer.instance.PlaySound(MusicPlayer.instance.bossKilled);
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
    public void KillPlayer(Collider2D player,string nome)
    {
        if(!block){
            MusicPlayer.instance.PlaySound(MusicPlayer.instance.playerDying);
            if(currentHealth > 0){
                currentHealth --;
                heartImages[currentHealth].enabled = false;
                Destroy(player.gameObject);
                block = true;
                ResetLevelCoins();
                if(currentHealth == 0){
                    playerDied = true;
                    TrocarCena("CenaFase1");   
                    ResetHealth(); 
                    ZerarCoins();
                    BossLifebar.SetActive(false);
                }else{
                    playerDied = true;
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

    public void BossHit()
    {
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
        SceneManager.LoadScene(nomeCena);

        if(nomeCena == "CenaInicial"){
            GameInterface.SetActive(false);
            BossLifebar.SetActive(false);
            lastScene.SetActive(false);
            orbCollected = false;
        }
        if(nomeCena=="CenaFinal"){
            GameInterface.SetActive(false);
            BossLifebar.SetActive(false);
            lastScene.SetActive(true);
            ShowLastMessage.text = ("CONGRATULATIONS "+PlayerPrefs.GetString("playerName")+", YOU BEATED THE GAME AND COLLECTED A TOTAL OF "+ PlayerPrefs.GetInt("totalCoins").ToString() + " MOON COINS, THAT'S AWESOME!");
            orbCollected = false;
        }
        if(nomeCena == "CenaBoss"){
            BossLifebar.SetActive(true);  
            GameInterface.SetActive(true);  
            currentBossHP = bossHealth;
            healthSlider.maxValue = 1000;
            healthSlider.value = currentBossHP;
            showBossHP.text = bossHealth.ToString();
        }

         if(nomeCena == "CenaFase1"){
            totalCoins = 0;
            GameInterface.SetActive(true);
            bossHealth = 1000;
        }

        if (isGameScene && currentHealth > 0 && !playerDied && !(nomeCena =="CenaFinal"))
        {
            string newSceneName = SceneManager.GetActiveScene().name;
            if (newSceneName != currentSceneName)
            {
                MusicPlayer.instance.PlaySound(MusicPlayer.instance.phasePassed);
            }
            currentSceneName = newSceneName;
        }

        playerDied = false;

        block = false;
        SaveData();
        currentCoins = 0;

    }
     public void StartGame()
    {
        TrocarCena("CenaFase1");
        heartImages = HealthPoints.GetComponentsInChildren<Image>();
        currentHealth = heartImages.Length; 
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

    public void Restarted(){
        bossHealth = 1000;
        totalCoins = 0;
        ResetHealth();
        ResetLevelCoins();
        LoadData();
    }
}
