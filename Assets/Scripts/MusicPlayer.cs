using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public GameObject maintheme;
    public GameObject phasestheme;
    public GameObject bosstheme;
    private string currentSceneName;
    private string currentThemeName;
    private bool inicio;

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
        currentSceneName = SceneManager.GetActiveScene().name;
        UpdateMusic();
        inicio = true;
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

        } else if (currentSceneName == "FasePreInicial"){
                StopAllMusic();
                maintheme.SetActive(true);

        }
        else if (currentSceneName == "FaseInicial"){
            if(!inicio){
                StopAllMusic();
                maintheme.SetActive(true);
            }else{
                inicio = false;
            }
        }else if (currentSceneName == "CenaBoss"){
                StopAllMusic();
                bosstheme.SetActive(true);
        }
    }

    void StopAllMusic()
    {
        maintheme.SetActive(false);
        phasestheme.SetActive(false);
        bosstheme.SetActive(false);
    }
}
