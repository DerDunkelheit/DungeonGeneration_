using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickupObjectBase,IPickupable
{
    [SerializeField] int healthToAdd = 1;

    Health playerHealth;

    public void Pickup() //Interface's method
    {
        playerHealth.Heal(healthToAdd);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<Health>();

            if(!playerHealth.CheckPlayerHealth())
            {
                Pickup();
                Destroy(this.gameObject);
            }
        }
    }
}
