using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemSlot : MonoBehaviour
{

    [SerializeField] Image Image;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    public Button dropItemButton;

    private Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
            {
                Image.color = disabledColor;
            }
            else
            {
                Image.sprite = _item.icon;
                Image.color = normalColor;
            }
        }
    }

      
  




}
