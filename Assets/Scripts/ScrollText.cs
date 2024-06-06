using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollText : MonoBehaviour
{
    public float scrollSpeed = 60f; 
    public RectTransform textPanel; 
    public RectTransform monitorImage; 
    public RectTransform skip; 
    private bool isScrolling = true; 

    void Update()
    {
        if (isScrolling)
        {
            textPanel.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            monitorImage.anchoredPosition -= new Vector2(0, scrollSpeed * Time.deltaTime);
            skip.anchoredPosition -= new Vector2(0, scrollSpeed * Time.deltaTime);

            if (textPanel.anchoredPosition.y >= 5000)
            {
                isScrolling = false; 
                SceneManager.LoadScene("CenaInicial");
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
                SceneManager.LoadScene("CenaInicial");
        }
    }
}