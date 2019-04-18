using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour
{

    [SerializeField] Image Image;

  

    public Button dropItemButton;

    public Item Item;

    public void AddItem(Item newitem)

    {
        Item = newitem;
        Image.sprite = Item.icon;
        Image.enabled = true;
        dropItemButton.interactable = true;


    }
    public void ClearSlot()
    {
        Item = null;
        Image.sprite = null;
        Image.enabled = false;
        dropItemButton.interactable = false;
    }
   




}
