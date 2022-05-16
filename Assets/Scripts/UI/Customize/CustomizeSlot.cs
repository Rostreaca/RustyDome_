using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomizeSlot : Slot, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null && count > 0)
                Inventory.instance.DisplayInform(item.itemInfo);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public override void UpdateSlot()
    {
        if (count > 0)
        {
            icon.sprite = item.icon;
            icon.color = Color.white;
        }

        else
        {
            icon.color = new Color(0, 0, 0, 0);
        }
    }
}
