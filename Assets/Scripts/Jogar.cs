using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class Jogar : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text avisoText;
    public string nome;
    public string charac;
    public Image maleCharacter;
    public Image femaleCharacter;

    public Color blinkColor = Color.white;
    public float blinkInterval = 0.5f;

    private Image selectedImage;
    private Color originalColor;
    private bool isBlinking = false;

    public string selectedCharacter;
    public static Jogar instance;


    void Start()
    {
        string nome = PlayerPrefs.GetString("playerName");
        string charac = PlayerPrefs.GetString("character");
        if (!string.IsNullOrEmpty(nome))
        {
            inputField.text = nome;
        }

        Scene currentScene = SceneManager.GetActiveScene(); 
        if(currentScene.name == "CenaInicial"){
            selectedImage = null;
            selectedCharacter = "";
            maleCharacter.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(maleCharacter, "male"));
            femaleCharacter.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(femaleCharacter, "female"));
        }
            instance = this;
    }

   public void Play()
    {
        string playerName = inputField.text;

        if (!string.IsNullOrEmpty(playerName) && !(selectedCharacter ==""))
        {
            PlayerPrefs.SetString("character", selectedCharacter);
            PlayerPrefs.SetString("playerName", playerName);
            PlayerPrefs.Save();

            GerenciadorDeJogo.instance.StartGame();
        }
        else
        {
            if(string.IsNullOrEmpty(playerName) && (selectedCharacter =="")){
               avisoText.text = "Please, Insert a name and select a character to play."; 
            }else if(string.IsNullOrEmpty(playerName)){
                avisoText.text = "Please, Insert a name to play.";
            }else{
                avisoText.text = "Please, Insert a character to play.";
            }
        }
    }
    void SelectCharacter(Image character, string characterType)
    {
        if (selectedImage != null)
        {
            StopAllCoroutines();
            selectedImage.color = originalColor;
        }

        selectedImage = character;
        originalColor = selectedImage.color;
        selectedCharacter = characterType;

        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        isBlinking = true;

        while (isBlinking)
        {
            selectedImage.color = blinkColor;
            yield return new WaitForSeconds(blinkInterval);
            selectedImage.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void OnDisable()
    {
        if (selectedImage != null)
        {
            StopAllCoroutines();
            selectedImage.color = originalColor;
        }
    }

     public string GetSelectedCharacter()
    {
        return selectedCharacter;
    }
    void SomeOtherMethod()
    {
        Jogar characterManager = FindObjectOfType<Jogar>();
        string selectedCharacter = characterManager.GetSelectedCharacter();

        if (selectedCharacter == "male")
        {

        }
        else if (selectedCharacter == "female")
        {

        }
    }
}

