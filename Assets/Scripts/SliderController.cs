using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.gameObject.SetActive(false);
    }

    void Update(){
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "CenaBoss")
        {
            slider.gameObject.SetActive(true);
        }
    }
}