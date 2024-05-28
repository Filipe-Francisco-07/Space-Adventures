using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CanvScript : MonoBehaviour
{
    public Slider slider;
    private static CanvScript instance = null;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (instance == null && !(currentScene.name =="CenaFinal" ))
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update(){
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "CenaFinal"){
            Destroy(gameObject);
        }
    }
}
