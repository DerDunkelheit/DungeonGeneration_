using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemType thisItemType = ItemType.Food;
    public int maxStack = 3;

    [TextArea(3,10)]public string itemDescription;

    Item item;
    bool canBePicken = false;

    Sprite currentSprite;

    UI_Inventory ui_Inventory;

    void Start()
    {
        ui_Inventory = FindObjectOfType<UI_Inventory>();

        if (ui_Inventory == null)
        {
            Debug.Log("Ui inventory is null");
        }
        
        item = new Item { itemType = thisItemType, amount = 1, maxStack = maxStack };

        currentSprite = this.GetComponent<SpriteRenderer>().sprite;

        StartCoroutine(waitBeforeSetPickupBool());
    }


    public void UpdateItem()
    {
        item.itemType = thisItemType;
        item.maxStack = maxStack;

        StartCoroutine(waitBeforeSetPickupBool());
    }

    private IEnumerator waitBeforeSetPickupBool()
    {
        yield return new WaitForSeconds(0.5f);
        canBePicken = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.TryGetComponent(out PlayerInventory playerInventory) && canBePicken)
        {
            for (int i = 0; i < playerInventory.inventory.slots.Length; i++)
            {
                if (playerInventory.inventory.isSlotFull[i] == false)
                {
                    playerInventory.inventory.isSlotFull[i] = true;

                    GameObject itemImageInInventory = Instantiate(ui_Inventory.itemSlotTemplatePrefab, playerInventory.inventory.slots[i].transform, false);
                    itemImageInInventory.transform.Find("image").GetComponent<Image>().sprite = item.GetSprite();
                    InventorySlot invSlot = playerInventory.inventory.slots[i].GetComponent<InventorySlot>();
                    invSlot.maxAmoutOnThisItemTypeInSlot = item.maxStack;
                    invSlot.thisSlotItemType = item.itemType;
                    invSlot.itemQueue.Enqueue(item);
                    invSlot.isEmpty = false;
                    invSlot.thisSlotSprite = currentSprite;
                    invSlot.thisItemDescription = itemDescription;

                    TextMeshProUGUI text = invSlot.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = invSlot.itemQueue.Count.ToString();

                    Destroy(this.gameObject);
                    break;
                }
                else
                {
                    InventorySlot invSlot = playerInventory.inventory.slots[i].GetComponent<InventorySlot>();
                    if(invSlot.itemQueue.Count < invSlot.maxAmoutOnThisItemTypeInSlot && invSlot.thisSlotItemType == item.itemType && playerInventory.inventory.slots[i].GetComponent<InventorySlot>().isEmpty == false)
                    {
                        invSlot.itemQueue.Enqueue(item);
                        TextMeshProUGUI text = invSlot.GetComponentInChildren<TextMeshProUGUI>();
                        text.text = invSlot.itemQueue.Count.ToString();

                        Destroy(this.gameObject);
                        break;
                    }
                }
            }
        }
    }

}
