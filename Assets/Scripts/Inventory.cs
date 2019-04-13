using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    GameObject inventoryPanel;
    GameObject slotPanel;
    GameObject inventorySlot;
    GameObject inventoryItem;


    public List<Item> items = new List<Item>();
    public List<GameObject> it = new List<GameObject>();

    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;

    }


}
