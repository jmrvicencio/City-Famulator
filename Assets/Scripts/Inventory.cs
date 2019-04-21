using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public List<Item> items;
   
    Inventory inventory;
    [SerializeField] Transform itemParent;
    public itemSlot[] itemSlots;




    //creating an event
    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    InventoryUI inventoryUI;


    #region Singleton

 
    public static Inventory instance;
    public void Awake()
    {
        itemSlots = itemParent.GetComponentsInChildren<itemSlot>();

        //to make a singleton
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;

        }
        instance = this;
    }

    #endregion

    public void onButtonClick(Item itemTooAdd)
    {
        AddItem(itemTooAdd.GetCopy());
    }
   
    //Adds the item to the first empty slot it finds
    public bool AddItem(Item item)
    {

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }

    
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
               itemSlots[i].Item = null;
                return true;
            }
        }
        return false;

    }

    public bool IsFull()
    {
        for (int i = 0; i <itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                
                return false;
            }
        }
        return true;

    }
}

