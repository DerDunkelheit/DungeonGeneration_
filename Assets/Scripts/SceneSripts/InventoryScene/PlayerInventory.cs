using System.Collections;
using System.Collections.Generic;
using CharacterCore;
using UnityEngine;

public class PlayerInventory : CharacterComponents
{
    [SerializeField] UI_Inventory uiInventory = null;
    public GameObject descriptionItemField = null;

     public Inventory inventory;

    bool isInventoryUiActive = false;

    void Awake()
    {
        uiInventory.SetInventory(inventory);
    }

    protected override void Start()
    {
        base.Start();

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
        if(Input.GetKeyDown(KeyCode.I))
        {
            isInventoryUiActive = !isInventoryUiActive;
            uiInventory.gameObject.SetActive(isInventoryUiActive);

            if(!isInventoryUiActive)
            {
                descriptionItemField.SetActive(false);
            }
        }
    }
}
