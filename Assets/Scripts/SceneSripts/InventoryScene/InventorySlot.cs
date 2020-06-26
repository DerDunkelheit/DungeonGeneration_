using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public bool isEmpty = true;
    public int maxAmoutOnThisItemTypeInSlot;
    public ItemType thisSlotItemType;


    public Queue<Item> itemQueue = new Queue<Item>();

    [HideInInspector] public Sprite thisSlotSprite;
    [TextArea(3, 10)] public string thisItemDescription;

    PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isEmpty)
            {
                Debug.Log($"in this slot {itemQueue.Count} {thisSlotItemType} to use");
                playerInventory.descriptionItemField.SetActive(true);
                Image itemImage = playerInventory.descriptionItemField.transform.Find("Item Image").GetComponent<Image>();
                TextMeshProUGUI descriptionText = playerInventory.descriptionItemField.transform.Find("Description TMP").GetComponent<TextMeshProUGUI>();
                descriptionText.text = thisItemDescription;
                itemImage.sprite = thisSlotSprite;

                Button itemUseButton = playerInventory.descriptionItemField.transform.Find("Use Button").GetComponent<Button>();
                itemUseButton.onClick.RemoveAllListeners();
                itemUseButton.onClick.AddListener(() => UseItem(itemQueue.Dequeue()));
                TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
                text.text = itemQueue.Count.ToString();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (itemQueue.Count > 0 && itemQueue != null)
            {
                playerInventory.descriptionItemField.SetActive(false);
                itemQueue.Dequeue();
                playerInventory.inventory.DropItem(thisSlotItemType);
                TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
                text.text = itemQueue.Count.ToString();
            }

            if (itemQueue.Count == 0 && !isEmpty)
            {

                GameObject itemSlotTemplate = this.transform.Find("ItemSlotTemplate(Clone)").gameObject;
                isEmpty = true;
                playerInventory.inventory.RefreshSloots();
                Destroy(itemSlotTemplate);
            }
        }

    }

    private void UseItem(Item test)
    {
        test.Use();

        if (itemQueue.Count > 0)
        {
            TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = itemQueue.Count.ToString();
        }

        if (itemQueue.Count == 0 && !isEmpty)
        {

            GameObject itemSlotTemplate = this.transform.Find("ItemSlotTemplate(Clone)").gameObject;
            isEmpty = true;
            playerInventory.inventory.RefreshSloots();
            playerInventory.descriptionItemField.SetActive(false);
            Destroy(itemSlotTemplate);
        }
    }


}
