using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    private TargetJoint2D tj;
    private BoxCollider2D bc;
    void Start()
    {
        tj = GetComponent<TargetJoint2D>();
        bc = GetComponent<BoxCollider2D>();
    }

   void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player")
        {
            Invoke("Cair",0.75f);
        }
       
    }
    void Cair(){
        tj.enabled = false;
        bc.isTrigger = true;
    }

}
