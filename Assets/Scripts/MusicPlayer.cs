using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public GameObject maintheme;
    public GameObject phasestheme;
    public GameObject bosstheme;
    private string currentSceneName;
    private string currentThemeName;
    private bool inicio;
    private bool final;
    public AudioSource jump;
    public AudioSource orbShoot;
    public AudioSource bossKilled;
    public AudioSource phasePassed;
    public AudioSource coin;
    public AudioSource enemyDying;
    public AudioSource playerDying;
    public AudioSource SwordAttack;
    public AudioSource BossKillSound;
    public AudioSource OpenChest;
    private bool tocandoFinal;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        instance = this;
        currentSceneName = SceneManager.GetActiveScene().name;
        UpdateMusic();
        inicio = true;
        final = false;
        tocandoFinal = false;
    }

    void Update()
    {
        string newSceneName = SceneManager.GetActiveScene().name;
        if (newSceneName != currentSceneName)
        {
            currentSceneName = newSceneName;
            UpdateMusic();
        }

        if(GerenciadorDeJogo.instance.zerou && !tocandoFinal){
            UpdateMusic();
        }

    }

    void UpdateMusic()
    {
        if (currentSceneName == "CenaFase1")
        {
            tocandoFinal = false;
            StopAllMusic();
            phasestheme.SetActive(true);
        }
        else if (currentSceneName == "CenaPreInicial")
        {
            StopAllMusic();
            maintheme.SetActive(true);
        }
        else if (currentSceneName == "CenaInicial")
        {
            tocandoFinal = false;
            if (!inicio && !final)
            {
                StopAllMusic();
                maintheme.SetActive(true);
            }
            else
            {
                inicio = false;
                final = false;
            }
        }
        else if (currentSceneName == "CenaBoss" && !GerenciadorDeJogo.instance.zerou)
        {
            StopAllMusic();
            bosstheme.SetActive(true);
        }
        else if(GerenciadorDeJogo.instance.zerou && !(currentSceneName == "CenaFinal")){
            StopAllMusic();
            final = true;
            maintheme.SetActive(true);
            tocandoFinal = true;
        }
    }

    void StopAllMusic()
    {
        maintheme.SetActive(false);
        phasestheme.SetActive(false);
        bosstheme.SetActive(false);
    }

    public void PlaySound(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(true);
        audioSource.Play();
    }

    IEnumerator DisableAudioSource(AudioSource audioSource, float duration)
    {
        yield return new WaitForSeconds(duration);
        audioSource.gameObject.SetActive(false);
    }
}