using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : CharacterComponents
{
    public UI_Inventory uiInventory = null;
    public GameObject descriptionItemField = null;

    public Inventory inventory;

    private bool isInventoryUiActive = false;

    private GameObject itemSlotContainer;

    void Awake()
    {
    }

    protected override void Start()
    {
        base.Start();

        SetupInventory();
        uiInventory.gameObject.SetActive(isInventoryUiActive);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void UseItem()
    {
        inventory.SendFucnToUseItemButton();
    }

    protected override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryUiActive = !isInventoryUiActive;
            uiInventory.gameObject.SetActive(isInventoryUiActive);

            if (!isInventoryUiActive)
            {
                descriptionItemField.SetActive(false);
            }
        }
    }

    private void SetInventorySlots()
    {
        inventory.slots[0] = itemSlotContainer.transform.Find("Slot 1").gameObject;
        inventory.slots[1] = itemSlotContainer.transform.Find("Slot 2").gameObject;
        inventory.slots[2] = itemSlotContainer.transform.Find("Slot 3").gameObject;
        inventory.slots[3] = itemSlotContainer.transform.Find("Slot 4").gameObject;
    }

    private void SetupInventory()
    {
        uiInventory = FindObjectOfType<UI_Inventory>();
        descriptionItemField = uiInventory.transform.Find("Item Description Field Background").gameObject;
        itemSlotContainer = uiInventory.transform.Find("ItemSlotContainer").gameObject;

        uiInventory.SetInventory(inventory);
        SetInventorySlots();
    }
}