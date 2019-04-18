using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] List<Item> items;
    [SerializeField] Transform itemParent;
    [SerializeField] itemSlot[] itemSlots;

    public void OnValidate()
    {
        
        if (itemParent != null)
        {
            itemSlots = itemParent.GetComponentsInChildren<itemSlot>();
        }
    }
    
    
    private void Update()
    {
      
        RefreshUI();

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
            
        RefreshUI();
    }
}

