using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCStranger : InteractiveObjectBase
{
    [System.Serializable]
    public class NPCPhrasClass
    {
        public string[] phrasesArray;
    }

    [Header("Phrases Fields")]
    [SerializeField] GameObject NPCPhrasesPanel = null;
    [SerializeField] TextMeshProUGUI phraseText = null;
    [SerializeField] NPCPhrasClass PhraseClass = null;
    [SerializeField] float minTimeToNextPhrase = 3;
    [SerializeField] float maxTimeToNextPhrase = 6;

    [Header("Dialog Settings")]
    [SerializeField] GameObject dialogPanel = null;
    [SerializeField] KeyCode openDialogKey = KeyCode.G;

    NPCDialogueHolder thisDialogueHolder;

    bool isDialoguePanelActive = false;

    Coroutine typeRoutine;

    protected override void Start()
    {
        base.Start();

        SetDialogPanel(false);
        NPCPhrasesPanel.SetActive(true);
        StartCoroutine(SayRandomPhrase(Random.Range(minTimeToNextPhrase, maxTimeToNextPhrase)));

        thisDialogueHolder = this.GetComponent<NPCDialogueHolder>();
    }

    void Update()
    {
        if(Input.GetKeyDown(openDialogKey) && isPlayerInRange)
        {
            SetDialogPanel(true);
            NPCPhrasesPanel.SetActive(false);

            thisDialogueHolder.TriggrDialogue(thisDialogueHolder.introducingDialogue); // trigger the introducing dialogue
        }

        if (thisDialogueHolder.isDialogueActive == false && isPlayerInRange && isDialoguePanelActive) // Dialogue Managment
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
               thisDialogueHolder.TriggrDialogue(thisDialogueHolder.howToPlayDialogue);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                thisDialogueHolder.TriggrDialogue(thisDialogueHolder.historyOfThisPlaceDialogue);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                
            }
        }
    }


    private void SetDialogPanel(bool value)
    {
        dialogPanel.SetActive(value);
        isDialoguePanelActive = value;
    }

    private IEnumerator SayRandomPhrase(float timeBetweenPhrases)
    {
        while (true)
        {
            int currentPhrase = Random.Range(0, PhraseClass.phrasesArray.Length);
            if(typeRoutine != null)
            {
                StopCoroutine(typeRoutine);
            }

            typeRoutine = StartCoroutine(TypeSentenceRoutine(PhraseClass.phrasesArray[currentPhrase], phraseText));
            yield return new WaitForSeconds(timeBetweenPhrases);
        }
    }

    private IEnumerator TypeSentenceRoutine(string sentence,TextMeshProUGUI textProp)
    {
        textProp.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textProp.text += letter;
            yield return new WaitForSeconds(0.015f);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.gameObject.CompareTag("Player"))
        {
            if (typeRoutine != null)
            {
                StopCoroutine(typeRoutine);
            }
            phraseText.text = $"Press{openDialogKey} to Speak with me!";
        }
    }



    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);

        if (other.gameObject.CompareTag("Player"))
        {
            SetDialogPanel(false);
            NPCPhrasesPanel.SetActive(true);
        }
    }

}
