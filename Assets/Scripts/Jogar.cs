using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Jogar : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text avisoText;
    public string nome;

    void Start()
    {
        string nome = PlayerPrefs.GetString("playerName");
        if (!string.IsNullOrEmpty(nome))
        {
            inputField.text = nome;
        }
    }

   public void Play()
    {
        string playerName = inputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("playerName", playerName);
            PlayerPrefs.Save();

            SceneManager.LoadScene("CenaFase1");
        }
        else
        {
            avisoText.text = "Please, Insert a name to play.";
        }
    }

}
