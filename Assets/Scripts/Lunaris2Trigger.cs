using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lunaris2Trigger : MonoBehaviour
{
    private bool InArea;
    public static bool entrouNave;
    private Animator anim;
    private GameObject player;
    public string flyAnimationName = "2LunarisFly";

    void Start(){
        anim = GetComponent<Animator>();
        entrouNave = false;
        InArea = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            InArea = true;
            player = collider.gameObject; 
            if (GerenciadorDeJogo.instance != null && GerenciadorDeJogo.instance.enterLunaris != null) {
                GerenciadorDeJogo.instance.enterLunaris.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            InArea = false;
            player = null; 
            if (GerenciadorDeJogo.instance != null && GerenciadorDeJogo.instance.enterLunaris != null) {
                GerenciadorDeJogo.instance.enterLunaris.SetActive(false);
            }
        }
    }

    void Update(){
        if(InArea){
            if(Input.GetKeyDown(KeyCode.E)){
                if (GerenciadorDeJogo.instance != null && GerenciadorDeJogo.instance.enterLunaris != null) {
                    GerenciadorDeJogo.instance.enterLunaris.SetActive(false);
                }

                if (player != null) {
                    player.SetActive(false);
                }

                if (anim != null) {
                    anim.SetBool("Idle", false);
                    anim.SetBool("Fly", true);
                    entrouNave = true;
                    StartCoroutine(WaitForAnimationToEnd(flyAnimationName));
                }
            }
        }
    }

    private IEnumerator WaitForAnimationToEnd(string animationName){
        float animationLength = GetAnimationClipLength(animationName);
        if (animationLength > 0) {
            yield return new WaitForSeconds(animationLength);
        }
        Destroy(gameObject);
        GerenciadorDeJogo.instance.Lunaris2.SetActive(false);
        GerenciadorDeJogo.instance.lastScene.SetActive(true);
        MusicPlayer.instance.PlaySound(MusicPlayer.instance.bossKilled);
    }

    private float GetAnimationClipLength(string animationName){
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips){
            if(clip.name == animationName){
                return clip.length;
            }
        }
        return 0f;
    }
}
