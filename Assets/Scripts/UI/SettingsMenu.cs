using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject panelMenu = null;

    bool isMenuActive = false;

    CharacterMovement characterMovement;

    void Start()
    {
        characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !isMenuActive;
        }

        if(isMenuActive)
        {
            panelMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            panelMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SetAnimation() //This Method in Toggle UI
    {
        characterMovement.enableAnimation = !characterMovement.enableAnimation;
    }

    public void StartNewGame() // This method is uded in start New Game Button
    {
        isMenuActive = false; //off the Munu and set time to normal sped
        Time.timeScale = 1;

        Destroy(AudioManager.instance.gameObject);
        Destroy(PlayerInstance.instance.gameObject);
        if (LevelManager.instance != null)
        {
            Destroy(LevelManager.instance.gameObject);
        }

        SceneManager.LoadScene("CharacterSelectScene");
    }
}
