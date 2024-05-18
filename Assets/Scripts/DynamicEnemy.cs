using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicEnemy : MonoBehaviour
{
    [SerializeField] float velocidade;
    private bool direcao;
    private float temp;
    [SerializeField] float tempo_vigilha = 2f;

    void Update()
    {
        if(direcao){
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        }else{
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
    }

    temp += Time.deltaTime;
    if(temp >= tempo_vigilha){
        direcao = !direcao;
        temp = 0;
    }
    
    }
}
