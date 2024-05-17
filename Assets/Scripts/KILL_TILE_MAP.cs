using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KILL_TILE_MAP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.CompareTag("Player")) {
        Destroy(collider.gameObject);
    }
}
}   
