using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] List<Item> items;
    [SerializeField] Transform itemParent;
    [SerializeField] itemSlot[] itemSlots;

    public void Start()
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
        
        for (int i = 0; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public void AddItem(Item item)
    {

        items.Add(item);
    }
}

