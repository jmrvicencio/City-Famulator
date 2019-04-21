using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemSlot : MonoBehaviour
{

    [SerializeField] Image Image;

  

    public Button dropItemButton;

    public Item Item;

    public void Awake()
    {
        
        
    }
    public void AddItem(Item newitem)
    {
        Item = newitem;
       
       
        Image.sprite = Item.icon;
        Image.enabled = true;
        dropItemButton.interactable = true;
       Debug.Log("Adding " + Item.name);

      
    }
    public void ClearSlot()
    {
        Inventory.instance.RemoveItem(Item);
        Item = null;
        Image.sprite = null;
        Image.enabled = false;
        dropItemButton.interactable = false;
    }
   
    public void onRemoveButton()
    {
       
        Inventory.instance.RemoveItem(Item);
    }




}
