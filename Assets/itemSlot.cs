using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour
{

    [SerializeField] Image Image;

  

    public Button dropItemButton;

    public Item Item;

    public void Awake()
    {
        dropItemButton.interactable = false;
        
    }
    public void AddItem(Item newitem)

    {
        Item = newitem;
        Image.sprite = Item.icon;
        Image.enabled = true;
        dropItemButton.interactable = true;
        Debug.Log("adding " + newitem);


    }
    public void ClearSlot()
    {
        Debug.Log("Trying to Remove" + Item);
        Inventory.instance.RemoveItem(Item);
        Item = null;
        Image.sprite = null;
        Image.enabled = false;
        dropItemButton.interactable = false;
    }
   




}
