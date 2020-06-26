using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] NPCDialogueHolder nPCDialogueHolder = null;

    [SerializeField] TextMeshProUGUI dialogueText = null;
    [SerializeField] GameObject dialogueDesisionHolder = null;

    Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log($"start dialogue with:{dialogue.NpcName}");
        dialogueDesisionHolder.SetActive(false);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() // this mehtod in continue button on dialogue Npc botton as well
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentenceRoutine(sentence));
    }

    private void EndDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentenceRoutine("Make a decision"));

        nPCDialogueHolder.isDialogueActive = false;
        dialogueDesisionHolder.SetActive(true);
    }


    private IEnumerator TypeSentenceRoutine(string  sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
