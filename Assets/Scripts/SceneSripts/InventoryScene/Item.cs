using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HealthPotion,
    ManaPotion,
    Coin,
    Food
};

[System.Serializable]
public class Item
{


    public ItemType itemType;
    public int amount;
    public int maxStack;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            case ItemType.HealthPotion:
                return ItemAssets.instance.healthPotionSprite;

            case ItemType.Coin:
                return ItemAssets.instance.coinSprite;

            case ItemType.ManaPotion:
                return ItemAssets.instance.manaPotionSprite;

            case ItemType.Food:
                return ItemAssets.instance.foodSprite;

            default:
                Debug.LogError("Error with Sprite Type");
                return null;
        }

    }

    public bool IsStackable()
    {
        switch(itemType)
        {
            case ItemType.HealthPotion:
                return true;

            case ItemType.Coin:
                return true;

            case ItemType.ManaPotion:
                return true;

            case ItemType.Food:
                return true;

            default:
                Debug.LogError($"Some king of error in stacable Fuhc in item class!");
                return false;
        }
    }

    public void Use()
    {
        switch(itemType)
        {
            case ItemType.HealthPotion:
                Debug.Log("HealthUsed");
                break;

            case ItemType.Food:
                Debug.Log("Food Used");
                break;

            case ItemType.ManaPotion:
                Debug.Log("Mana Used");
                break;
        }
    }
}
