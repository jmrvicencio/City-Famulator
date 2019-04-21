using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public static InventoryUI instance;
    Inventory inventory;
    [SerializeField] Transform itemParent;
    public itemSlot[] itemSlots;

    private void Start()
    {
        instance = this;
        //creating a reference to the inventory script
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        itemSlots = itemParent.GetComponentsInChildren<itemSlot>();
    }


    void UpdateUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {

                itemSlots[i].AddItem(inventory.items[i]);

            }
            else
            {
                itemSlots[i].ClearSlot();
            }
           
        }
    }
}

