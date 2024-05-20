using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvScript : MonoBehaviour
{
    private static CanvScript instance = null;
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
        }
    }
}
