using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public bool[] isSlotFull;
    public GameObject[] slots = null;

    public event EventHandler OnItemListChanged;

    public List<Item> itemList;

    [HideInInspector] public Item itemToUseInUseButton;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void SendFucnToUseItemButton()
    {
        itemToUseInUseButton.Use();
    }

    public void AddItem(Item newItem)
    {
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RefreshSloots()
    {
        for (int i = 0; i < isSlotFull.Length; i++)
        {
            InventorySlot invSlot = slots[i].GetComponent<InventorySlot>();
            if(invSlot.isEmpty)
            {
                isSlotFull[i] = false;
            }
        }
    }

    public void TriggerEvent()
    {
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void DropItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Coin:

                break;

            case ItemType.Food:
                string FoodDescription = "Food. adds one amount of hunget to player.";
                GameObject pickupableFood = new GameObject();
                SetItemConfigurationsForDropItemFuck(pickupableFood, ItemAssets.instance.foodSprite, "Food PickUp", ItemType.Food, 4, FoodDescription);
                break;

            case ItemType.HealthPotion:
                string healthDescription = "HealthPotion adds one health point to player";
                GameObject pickupableHelathPotion = new GameObject();
                SetItemConfigurationsForDropItemFuck(pickupableHelathPotion, ItemAssets.instance.healthPotionSprite, "Health Potion Pickup", ItemType.HealthPotion, 3, healthDescription);
                break;

            case ItemType.ManaPotion:
                string manaDescription = "Restore 1 point of mana";
                GameObject pickupableManaPotion = new GameObject();
                SetItemConfigurationsForDropItemFuck(pickupableManaPotion, ItemAssets.instance.manaPotionSprite, "Mana Potion PickUp", ItemType.ManaPotion, 3,manaDescription);
                break;
        }
    }

    private void SetItemConfigurationsForDropItemFuck(GameObject pickupableItem, Sprite itemSprite, string itemName, ItemType itemType, int itemMaxStack,string itemDescription)
    {
        pickupableItem.transform.position = PlayerInstance.instance.transform.position;
        pickupableItem.AddComponent<SpriteRenderer>().sprite = itemSprite;
        pickupableItem.AddComponent<Rigidbody2D>().isKinematic = true;
        pickupableItem.AddComponent<BoxCollider2D>().isTrigger = true;
        pickupableItem.GetComponent<BoxCollider2D>().size = Vector2.one * 0.8f;
        ItemPickup itemPickup = pickupableItem.AddComponent<ItemPickup>();
        itemPickup.name = itemName;
        itemPickup.thisItemType = itemType;
        itemPickup.maxStack = itemMaxStack;
        itemPickup.itemDescription = itemDescription;
    }
}
