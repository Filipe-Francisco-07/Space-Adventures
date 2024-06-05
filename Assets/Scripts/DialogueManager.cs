using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text speakerNameText;
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f; // Velocidade da digitação

    private Queue<string> sentences;
    private Queue<string> speakers;
    private bool isDialogueActive;
    public GameObject player;
    public GameObject boss;

    void Start()
    {
        sentences = new Queue<string>();
        speakers = new Queue<string>();
        isDialogueActive = false;
        if(!GerenciadorDeJogo.instance.dialogou){
            StartCoroutine(SetupBossScene());
        }
    }
    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence(player, boss);
        }
    }

      private IEnumerator SetupBossScene()
    {
        GerenciadorDeJogo.instance.playermoveblock = true;
        GerenciadorDeJogo.instance.bossmoveblock = true;
        yield return new WaitForSeconds(0.1f);

        boss = GameObject.FindWithTag("Boss");
        player = GameObject.FindWithTag("Player");

        if (boss != null)
        {
            boss.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        if (player != null)
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        
        StartDialogue(player,boss);
    }

    public void EnableControls(GameObject player,GameObject boss)
    {
        if (boss != null)
        {
            boss.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; 
        }

        if (player != null)
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void StartDialogue(GameObject player,GameObject boss)
    {
        dialoguePanel.SetActive(true);
        sentences.Clear();
        speakers.Clear();

        speakers.Enqueue("Player");
        sentences.Enqueue("So it was you who stole my ship! I ask you to get out of the way or I will have to defeat you");
        
        speakers.Enqueue("Boss");
        sentences.Enqueue("I'm impressed that you made it this far to get your ship back, but now you will die mercilessly");
        
        speakers.Enqueue("Player");
        sentences.Enqueue("That's what we'll see");

        isDialogueActive = true;
        DisplayNextSentence(player, boss);
    }

    public void DisplayNextSentence(GameObject player,GameObject boss)
    {
        if (sentences.Count == 0)
        {
            EndDialogue(player,boss);
            return;
        }

        string speaker = speakers.Dequeue();
        string sentence = sentences.Dequeue();
        speakerNameText.text = speaker;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue(GameObject player,GameObject boss)
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        EnableControls(player,boss); 
        GerenciadorDeJogo.instance.dialogou = true;
        GerenciadorDeJogo.instance.playermoveblock = false;
        GerenciadorDeJogo.instance.bossmoveblock = false;

    }
}