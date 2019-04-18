using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour
{
  
    [SerializeField] Image Image;

    public Sprite emptySlot;

    
    private Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                Image.overrideSprite = Item.icon;
                Image.enabled = true;
                
               
                
            }
            else
            {
                Image.enabled = false;

            }
        }
    }
    private void OnValidate()
    {
        if (Image == null)
        {
            Image = GetComponent<Image>();
        }
    }






}
