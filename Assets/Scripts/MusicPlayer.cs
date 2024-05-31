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
    }

    void Update()
    {
        string newSceneName = SceneManager.GetActiveScene().name;
        if (newSceneName != currentSceneName)
        {
            currentSceneName = newSceneName;
            UpdateMusic();
        }
    }

    void UpdateMusic()
    {
        if (currentSceneName == "CenaFase1")
        {
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
        else if (currentSceneName == "CenaBoss")
        {
            StopAllMusic();
            bosstheme.SetActive(true);
        }
        else if (currentSceneName == "CenaFinal")
        {
            StopAllMusic();
            maintheme.SetActive(true);
            final = true;
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