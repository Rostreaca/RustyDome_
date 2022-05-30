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
        if (this.item == null || count < 1)
            return;

        icon.color = Color.gray;
        HandManager.instance.TakeItem(item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandManager.instance.UpdateHand();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.color = Color.white;
        HandManager.instance.Drop();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //ÀåÂø½½·Ô ¿Ü ¹ÝÀÀX
        if (type != SlotType.ModuleEquipSlot)
            return;

        if (HandManager.instance.item != null)
        {
            Equip(HandManager.instance.item);
        }
    }

    public void Equip(Item item)
    {
        this.item = item;
        count = 1;

        UpdateSlot();
        PlayerController.instance.StateUpdate();
        Customize.instance.UpdateOccupancy();
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
