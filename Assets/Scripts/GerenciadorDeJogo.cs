using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GerenciadorDeJogo : MonoBehaviour
{
    public int totalCoins, collectedCoins, currentCoins, bossHealth = 1000;
    public TMP_Text showCoins, ShowLastMessage, NameStats, CoinStats, CharStats, showBossHP;
    public GameObject HealthPoints, pause, lastScene, GameInterface, BossLifebar, playerStats, openChest;
    public GameObject orbReceive, maleCharacterPrefab, femaleCharacterPrefab, fade, Lunaris2, enterLunaris, irFinal;
    public bool orbCollected, isGameScene, playermoveblock, bossmoveblock, dialogou, zerou, lookingStats, resetou;
    private GameObject currentCharacter;
    private int currentHealth, totalCollected, currentBossHP;
    private Image[] heartImages;
    private bool block, paused, playerDied = false;
    public Slider healthSlider;
    public string character;
    private string selectedCharacter, currentSceneName = "";
    private Animator fadeAnimator;
    public static GerenciadorDeJogo instance;
    void Start()
    {
        resetou = false;
        playermoveblock = false;
        bossmoveblock = false;
        fadeAnimator = fade.GetComponent<Animator>();
        dialogou = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
        zerou = false;
        orbCollected = false;
        lookingStats= false;
        instance = this;
        block = false;
        paused = false;
        LoadData();
    }
    void Update(){
        Scene currentScene = SceneManager.GetActiveScene();

        isGameScene = currentScene.name.StartsWith("CenaFase") || currentScene.name == "CenaPreBoss" ||  currentScene.name == "CenaBoss" || (currentScene.name == "CenaFinal" && !Lunaris2Trigger.estafinal);
        if(isGameScene && !paused){
            if(Input.GetKeyDown(KeyCode.Escape)){
                Pause();
            }
        }else if(paused){
            if(Input.GetKeyDown(KeyCode.Escape) && !lookingStats){
                Resume();
            }else if(lookingStats){
                HideStats();
            }
        }else if(currentScene.name == "CenaFinal" && lookingStats){
            if(Input.GetKeyDown(KeyCode.Escape)){
                HideStats();
                lastScene.SetActive(true);
            }
        }
    }

    public void CollectedOrb()
    {
        orbCollected = true;
        orbReceive.SetActive(false);
        if (currentCharacter != null)
        {
            ActivateOrb(currentCharacter, orbCollected);
        }
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
        resetou = true;
        Scene currentScene = SceneManager.GetActiveScene();
        TrocarCena(currentScene.name);
        pause.SetActive(false);
        Time.timeScale = 1;   
        paused = false;
        resetou = false;
    }

    public void TotalRestart()
    {
        SaveData();
        ZerarCoins();
        ResetHealth();
        TrocarCena("CenaFase1");
        Lunaris2Trigger.estafinal = false;
        pause.SetActive(false);
        GameInterface.SetActive(true);
        BossLifebar.SetActive(false);
        lastScene.SetActive(false);
        orbCollected = false;
        zerou = false;
        resetou = true;
    }

    public void MainMenu()
    {
        Lunaris2Trigger.estafinal = false;
        Scene currentScene = SceneManager.GetActiveScene();
        pause.SetActive(false);
        GameInterface.SetActive(false);
        BossLifebar.SetActive(false);
        lastScene.SetActive(false);
        orbCollected = false;
        zerou = false;
        SaveData();
        ZerarCoins();
        ResetHealth();
        if (currentScene.name != "CenaFinal")
        {
           Resume();
        }
        TrocarCena("CenaInicial");
        selectedCharacter="";
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
        CharStats.text = PlayerPrefs.GetString("character");
        lookingStats = true;
    }
    public void HideStats()
    {
        Time.timeScale = 1;
        playerStats.SetActive(false);
        lookingStats = false;
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "CenaFinal" && Lunaris2Trigger.estafinal){
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
        zerou = true;
        MusicPlayer.instance.PlaySound(MusicPlayer.instance.BossKillSound);
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

        if(nomeCena=="CenaFinal"){
            if(dialogou){
                dialogou = false;
            }
            SaveData();
            UpdateCoins();
            GameInterface.SetActive(false);
            BossLifebar.SetActive(false);
            Lunaris2.SetActive(true);
            lastScene.SetActive(false);
            ShowLastMessage.text = ("CONGRATULATIONS "+PlayerPrefs.GetString("playerName")+", YOU BEATED THE GAME AND COLLECTED A TOTAL OF "+ (PlayerPrefs.GetInt("totalCoins")+5000).ToString() + " MOON COINS, THAT'S AWESOME!");
        }
        if(nomeCena == "CenaBoss"){
            StartCoroutine(Fade());
            BossLifebar.SetActive(true);  
            GameInterface.SetActive(true);  
            currentBossHP = bossHealth;
            healthSlider.maxValue = 1000;
            healthSlider.value = currentBossHP;
            showBossHP.text = bossHealth.ToString();
        }

         if(nomeCena == "CenaFase1"){
            StartCoroutine(Fade());
            totalCoins = 0;
            GameInterface.SetActive(true);
            bossHealth = 1000;
            Lunaris2Trigger.estafinal = true;
        }

        if (isGameScene && currentHealth > 0 && !playerDied && !(nomeCena =="CenaFinal")&& !(nomeCena =="CenaInicial")  && !(resetou))
        {
            StartCoroutine(Fade());
            string newSceneName = SceneManager.GetActiveScene().name;
            if ((newSceneName != currentSceneName))
            {
                MusicPlayer.instance.PlaySound(MusicPlayer.instance.phasePassed);
            }
            if(currentSceneName != newSceneName){
                currentSceneName = newSceneName;  
            }
        }
        playerDied = false;
        block = false;
        SaveData();
        currentCoins = 0;
    }
      private IEnumerator Fade()
    {
        float leng = 0;
        AnimationClip[] clips = fadeAnimator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips){
            if(clip.name == "fade"){
                leng = clip.length;
            }
        }
        fade.SetActive(true);
        fadeAnimator.Play("fade");
        yield return new WaitForSeconds(leng - 0.10f);
        fade.SetActive(false);
    }
     public void StartGame()
    {
        TrocarCena("CenaFase1");
        heartImages = HealthPoints.GetComponentsInChildren<Image>();
        currentHealth = heartImages.Length; 
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.StartsWith("CenaFase") || scene.name == "CenaPreBoss" ||  scene.name == "CenaBoss" || (scene.name == "CenaFinal" && !Lunaris2Trigger.entrouNave)){
            selectedCharacter = PlayerPrefs.GetString("character");
            InstantiateCharacter(selectedCharacter);
        }
    }

    private Transform FindSpawnPoint()
    {
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            return spawnPoint.transform;
        }
        else
        {
            return null;
        }
    }
    public void InstantiateCharacter(string characterName)
    {
        Transform spawnPoint = FindSpawnPoint();

        if (spawnPoint != null)
        {
            if (characterName == "male")
            {
                currentCharacter = Instantiate(maleCharacterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else if (characterName == "female")
            {
                currentCharacter = Instantiate(femaleCharacterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }

        ActivateOrb(currentCharacter, orbCollected);
    }

     public void ActivateOrb(GameObject character, bool isActive)
    {
        Transform orbTransform = character.transform.Find("orb");
        if (orbTransform != null)
        {
            orbTransform.gameObject.SetActive(isActive);
        }
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
