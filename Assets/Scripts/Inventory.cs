using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    GameObject inventoryPanel;
    GameObject slotPanel;


    public GameObject inventorySlot;
    public GameObject inventoryItem;

    // reference to the Item Database script
    ItemDatabase database;

    // assigning the max amount of slots
    int slotAmount;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        // grab the Item Database component
        database = GetComponent<ItemDatabase>();

        // reference for the inventory panel adn slot Panel
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;

        slotAmount = 21;

        for (int i = 0; i < slotAmount; i++)
        {
            //adds a blank item with the id of -2 
            items.Add(new Item());
            // adds a game object that holds the graphic for the slot panels
            slots.Add(Instantiate(inventorySlot));
            //parent the slots to the slot Panel
            slots[i].transform.SetParent(slotPanel.transform);


        }

        AddItem(0);
        AddItem(1);
    }

    public void AddItem(int id)
    {
        // make a variable to store info recieved from the database using FetchItemByID
        Item ItemtoAdd = database.FetchItemByID(id);

        // loop through the current items list
        for (int i = 0; i < items.Count; i++)
        {
            //if the item is an empty item
            if (items[i].Id == -2)
            {
                items[i].Id = ItemtoAdd.Id;
                //create a new game object to store the graphic using the inventoryItem prefab
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.position = Vector2.zero;
                itemObj.GetComponent<Image>().sprite = ItemtoAdd.itemSprite;
                break;

            }
        }
    }

}
