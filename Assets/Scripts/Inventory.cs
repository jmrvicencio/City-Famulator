using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] List<Item> items;
    [SerializeField] Transform itemParent;
    [SerializeField] itemSlot[] itemSlots;


    public static Inventory instance;

    public void OnValidate()
    {
        
        if (itemParent != null)
        {
            itemSlots = itemParent.GetComponentsInChildren<itemSlot>();
        }
    }


    public void Awake()
    {
        //to make a singleton
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;

        }
        instance = this;
    }
    private void Update()
    {
      
       

    }

    private void RefreshUI()
    {
        
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if ( i < items.Count)
            {

            itemSlots[i].AddItem(items[i]);

            }
           
        }
        
    }

    public void AddItem(Item item)
    {

        items.Add(item);
        RefreshUI();
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        RefreshUI();
    }
}

