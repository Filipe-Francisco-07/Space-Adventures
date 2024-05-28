using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider; 

    private void Start()
    {
        Debug.Log("oi");
        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("oi");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("oi");
        if (scene.name == "CenaBoss")
        {
            Debug.Log("oi");
            if (slider != null)
            {
                slider.gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}