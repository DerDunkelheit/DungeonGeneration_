using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public GameObject itemSlotTemplatePrefab = null;

    Inventory inventory;
    Transform itemSlotContainer;

    void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
    }

    public void SetInventory(Inventory _inventory)
    {
        this.inventory = _inventory;

        inventory.OnItemListChanged += InventoryOnListChanged;
    }

    private void InventoryOnListChanged(object sender, System.EventArgs e)
    {
      
    }

   

}
