using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHam : PickupObjectBase,IPickupable
{
    [SerializeField] int hungerToAdd = 1;

    Hunger playerHunger;

    public void Pickup()
    {
        playerHunger.AddHunget(hungerToAdd);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerHunger = other.GetComponent<Hunger>();

            if(playerHunger.CurrentHunger != playerHunger.maxHunger)
            {
                Pickup();
                Destroy(this.gameObject);
            }
        }
    }

}
