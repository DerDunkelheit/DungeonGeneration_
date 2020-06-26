using System.Collections;
using System.Collections.Generic;
using CharacterCore;
using Player_Abilities_Stats.Health;
using Player_Abilities_Stats.Hunger;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [Header("Next Scene Name")]
    [SerializeField] string nextSceneToLoad = "";

    [Header("General Settings")]
    [SerializeField] GameObject characterPrefab = null;
    [SerializeField] Sprite[] characterSpritesArray = null;
    [SerializeField] float timeBeforeLoadNextScene = 2f;

    [Header("UI Settings")]
    [SerializeField] TextMeshProUGUI titleText = null;

    SpriteRenderer characterRenderer;
    bool isCharacterSelected = false;
    int userChoice;

    void Awake()
    {
        characterRenderer = characterPrefab.GetComponentInChildren<SpriteRenderer>();
        isCharacterSelected = false;
    }

    void Update()
    {
        if (!isCharacterSelected)
        {
            if (Input.GetKeyDown("1"))
            {
                isCharacterSelected = true;
                characterRenderer.sprite = characterSpritesArray[0];
                SetCharacterStats(4, 5, 5);
                StartCoroutine(waitBeforeLoadNextScene(timeBeforeLoadNextScene));
                userChoice = 1;
                UpdateTitleUI();
            }
            if (Input.GetKeyDown("2"))
            {
                isCharacterSelected = true;
                characterRenderer.sprite = characterSpritesArray[1];
                SetCharacterStats(3, 7, 5.5f);
                StartCoroutine(waitBeforeLoadNextScene(timeBeforeLoadNextScene));
                userChoice = 2;
                UpdateTitleUI();
            }
            if (Input.GetKeyDown("3"))
            {
                isCharacterSelected = true;
                characterRenderer.sprite = characterSpritesArray[2];
                SetCharacterStats(6, 3, 4.3f);
                StartCoroutine(waitBeforeLoadNextScene(timeBeforeLoadNextScene));
                userChoice = 3;
                UpdateTitleUI();
            }
        }
    }

    private void SetCharacterStats(int initialHelath, int initialHunger, float startWalkSpeed)
    {
        characterPrefab.GetComponent<CharacterMovement>().startWalkSpeed = startWalkSpeed;
        characterPrefab.GetComponent<Health>().maxHealth = initialHelath;
        characterPrefab.GetComponent<Hunger>().maxHunger = initialHunger;
        characterPrefab.GetComponent<Hunger>().enableHunger = false; //Disable hunger in Scene with NPC, the hunger will on when player leave scene with NPC
    }

    private void UpdateTitleUI()
    {
        titleText.text = $"You've selected:{userChoice} Character";
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }

    private IEnumerator waitBeforeLoadNextScene(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }
}
