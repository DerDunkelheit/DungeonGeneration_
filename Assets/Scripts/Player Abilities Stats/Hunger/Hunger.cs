using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public bool enableHunger = true;

    public int maxHunger = 5;
    [Tooltip("In Seconds")] [Range(5, 100)] [SerializeField] float hungerTick = 30f;
    [Tooltip("In Seconds")] [SerializeField] float healthTick = 30f;

    [HideInInspector] public bool canTakeDamageFromHunger;

    Health playerHealth;
    int currentHunget;
    Coroutine hugerRoutine;
    float time;

    public int CurrentHunger
    {
        get => currentHunget;
        set
        {
            if (value > maxHunger)
            {
                currentHunget = maxHunger;
            }
            else
            {
                currentHunget = value;
            }

        }
    }

    void Start()
    {
        playerHealth = this.GetComponent<Health>();

        CurrentHunger = maxHunger;
        UIManager.Instance.UpdatePlayerHungerUI(CurrentHunger, maxHunger);

    
            hugerRoutine = StartCoroutine(IEHunger(hungerTick));
            time = hungerTick;
        
    }

    void Update()
    {
        if (canTakeDamageFromHunger && enableHunger)
        {
            if (time <= 0)
            {
                playerHealth.TakeDamage(1);
                time = healthTick;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    public void AddHunget(int amount)
    {
        CurrentHunger += amount;
        UIManager.Instance.UpdatePlayerHungerUI(CurrentHunger, maxHunger);

        if (CurrentHunger > 0)
        {
            canTakeDamageFromHunger = false;
            time = healthTick;
        }
    }



    private IEnumerator IEHunger(float tickTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(tickTime);

            if (CurrentHunger > 0 && enableHunger)
            {
                CurrentHunger--;
                UIManager.Instance.UpdatePlayerHungerUI(CurrentHunger, maxHunger);
            }

            if (CurrentHunger == 0)
            {
                canTakeDamageFromHunger = true;
            }
        }
    }


}
