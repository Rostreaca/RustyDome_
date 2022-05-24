using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomizeSlot : Slot, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public enum SlotType
    {
        ModuleEquipSlot,
        ModuleSlot
    }

    public SlotType type;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null && count > 0)
                Customize.instance.DisplayInform(item.itemInfo);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.item == null)
            return;

        icon.color = Color.gray;
        HandManager.instance.TakeItem(item);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HandManager.instance.Drop();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //ÀåÂø½½·Ô ¿Ü ¹ÝÀÀX
        if (type != SlotType.ModuleEquipSlot)
            return;

        if (HandManager.instance.item != null)
        {
            Add(HandManager.instance.item);
        }
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

        }
    }
}
