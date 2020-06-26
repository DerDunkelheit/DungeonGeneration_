using CharacterCore;
using UI;
using UnityEngine;

namespace Player_Abilities_Stats.Health
{
    public class Health : MonoBehaviour, IDamageable, ICurable
    {
        [Header("General Settings")]
        public int maxHealth = 4;


        int currentHealth;
        Character character;
        CharacterMovement characterMovement;

        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (value >= maxHealth)
                {
                    currentHealth = maxHealth;
                }
                else
                {
                    currentHealth = value;
                }
            }
        }

        void Awake()
        {
            CurrentHealth = maxHealth;
            characterMovement = this.GetComponent<CharacterMovement>();
            character = this.GetComponent<Character>();

            if (character.characterType == Character.CharacterType.Player)
            {
                UIManager.Instance.UpdatePlayerHealthUI(CurrentHealth, maxHealth);
            }
        }



        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TakeDamage(1);
            }
        }

        /// <summary>
        /// if player has max health, this method retruns true.
        /// </summary>
        /// <returns></returns>
        public bool CheckPlayerHealth()
        {
            if (CurrentHealth == maxHealth)
            {
                return true;
            }

            return false;
        }

        public void TakeDamage(int damage) //Interface's method
        {
            CurrentHealth -= damage;

            if (character.characterType == Character.CharacterType.Player)
            {
                UIManager.Instance.UpdatePlayerHealthUI(CurrentHealth, maxHealth);
            }

            if (character.characterType == Character.CharacterType.Player)
            {
                if (CurrentHealth <= 0)
                {
                    CurrentHealth = 0;
                    Die();
                }
            }
            else
            {
                if (CurrentHealth <= 0)
                {
                    Destroy(this.gameObject);
                    LevelManager.instance.ReduceEnemiesCountVariable();
                }
            }

        }

        public void Heal(int amount) //Interface's method
        {
            CurrentHealth += amount;
            UIManager.Instance.UpdatePlayerHealthUI(CurrentHealth, maxHealth);
        }

        private void Die()
        {
            Debug.Log("You are Dead");
            DeathMenu.Instance.SetDeathPanel(true);
            characterMovement.enabled = false;
        }


    }
}
