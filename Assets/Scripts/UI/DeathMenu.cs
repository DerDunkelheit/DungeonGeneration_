using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathMenu : Singleton<DeathMenu>
{
    [SerializeField] GameObject deathPanel = null;

    void Start()
    {
        deathPanel.SetActive(false);
    }


    /// <summary>
    /// Sets GO deathPanel selected state
    /// </summary>
    /// <param name="state">true sets to active, false sets to inactive</param>
    public void SetDeathPanel(bool state)
    {
        deathPanel.SetActive(state);
    }

    public void StartNewGame() // You have to destroy all Objects which have dontDestroyOnLoad, if you want to restart a game.                      
    {                          // This method is used on start new game button.
        Destroy(AudioManager.instance.gameObject);
        Destroy(PlayerInstance.instance.gameObject);
        Destroy(LevelManager.instance.gameObject);
        SceneManager.LoadScene("CharacterSelectScene");
    }

    public void Exit()// This method is used on Exit button.
    {
        Application.Quit();
    }
    
   
}
