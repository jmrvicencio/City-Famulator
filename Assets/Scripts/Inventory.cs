using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] List<Item> items;
    [SerializeField] Transform itemParent;
    [SerializeField] itemSlot[] itemSlots;

    private void OnValidate()
    {
        if (itemParent != null)
        {
            itemSlots = itemParent.GetComponentsInChildren<itemSlot>();
        }
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


}

