using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueHolder : MonoBehaviour
{

    [Header("Introductory Dialogue")]
    public Dialogue introducingDialogue = null;

    [Header("Others Dialogues")]
    public Dialogue howToPlayDialogue = null;
    public Dialogue historyOfThisPlaceDialogue = null;

    public bool isDialogueActive = false;




    public void TriggrDialogue(Dialogue dialogueToTrigger)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueToTrigger);
        isDialogueActive = true;
    }
}
