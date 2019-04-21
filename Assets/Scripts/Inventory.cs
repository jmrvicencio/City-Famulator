using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public List<Item> items;
 



    //creating an event
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    InventoryUI inventoryUI;


    #region Singleton

    public static Inventory instance;
    public void Awake()
    {
        inventoryUI = InventoryUI.instance;
        //to make a singleton
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;

        }
        instance = this;
    }

    #endregion


   
    //Adds the item to the first empty slot it finds
    public bool AddItem(Item item)
    {

        for (int i = 0; i < inventoryUI.itemSlots.Length; i++)
        {
            if(inventoryUI.itemSlots[i].Item == null)
            {
                inventoryUI.itemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }

    
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < inventoryUI.itemSlots.Length; i++)
        {
            if (inventoryUI.itemSlots[i].Item == item)
            {
                inventoryUI.itemSlots[i].Item = null;
                return true;
            }
        }
        return false;

    }

    public bool IsFull()
    {
        for (int i = 0; i < inventoryUI.itemSlots.Length; i++)
        {
            if (inventoryUI.itemSlots[i].Item == null)
            {
                
                return false;
            }
        }
        return true;

    }
}

