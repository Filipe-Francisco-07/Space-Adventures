using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canv_Controller : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
