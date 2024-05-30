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
        if (IsMainThemeScene(currentSceneName))
        {
            if (currentThemeName != "main")
            {
                StopAllMusic();
                maintheme.SetActive(true);
                currentThemeName = "main";
            }
        }
        else if (IsPhaseThemeScene(currentSceneName))
        {
            if (currentThemeName != "phase")
            {
                if (currentThemeName != "phase")
                {
                    StopAllMusicExcept("phase");
                }
                phasestheme.SetActive(true);
                currentThemeName = "phase";
            }
        }
        else if (currentSceneName == "CenaBoss")
        {
            if (currentThemeName != "boss")
            {
                StopAllMusic();
                bosstheme.SetActive(true);
                currentThemeName = "boss";
            }
        }
    }

    void StopAllMusic()
    {
        maintheme.SetActive(false);
        phasestheme.SetActive(false);
        bosstheme.SetActive(false);
    }

    void StopAllMusicExcept(string themeToKeep)
    {
        if (themeToKeep != "main")
            maintheme.SetActive(false);
        if (themeToKeep != "phase")
            phasestheme.SetActive(false);
        if (themeToKeep != "boss")
            bosstheme.SetActive(false);
    }

    bool IsMainThemeScene(string sceneName)
    {
        return sceneName == "CenaInicial" || sceneName == "CenaFinal";
    }

    bool IsPhaseThemeScene(string sceneName)
    {
        return sceneName.StartsWith("CenaFase") || sceneName == "CenaPreBoss";
    }
}
