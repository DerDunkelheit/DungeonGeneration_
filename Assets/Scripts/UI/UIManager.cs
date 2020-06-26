using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Health Fields")]
    [SerializeField] Image healthBar = null;
    [SerializeField] TextMeshProUGUI healthText = null;

    [Header("Hunger Fields")]
    [SerializeField] Image hungerBar = null;
    [SerializeField] TextMeshProUGUI hungerText = null;


    float playerCurrentHealth;
    float playerMaxHealth;

    float playerCurrentHunger;
    float playerMaxHunger;

    Health playerHealth;
    Hunger playerHunger;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        playerHunger = playerHealth.GetComponent<Hunger>();

        UpdatePlayerHealthUI(playerHealth.CurrentHealth, playerHealth.maxHealth);
        UpdatePlayerHungerUI(playerHunger.CurrentHunger, playerHunger.maxHunger);
    }


    void Update()
    {
        InternalUpdate();
    }

    public void UpdatePlayerHealthUI(int currentHealth, int maxHealth)
    {
        playerCurrentHealth = currentHealth;
        playerMaxHealth = maxHealth;
    }

    public void UpdatePlayerHungerUI(int currentHunget, int maxHunger)
    {
        playerCurrentHunger = currentHunget;
        playerMaxHunger = maxHunger;
    }

    private void InternalUpdate()
    {
        //Health
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerCurrentHealth / playerMaxHealth, 10f * Time.deltaTime);
        healthText.text = playerCurrentHealth.ToString() + "/" + playerMaxHealth.ToString();

        //Hunger
        hungerBar.fillAmount = Mathf.Lerp(hungerBar.fillAmount, playerCurrentHunger / playerMaxHunger, 10f * Time.deltaTime);
        hungerText.text = playerCurrentHunger.ToString() + "/" + playerMaxHunger.ToString();
    }


}
