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
    public bool isEquiped;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null && count > 0)
            {
                Customize.instance.DisplayInform(item.itemInfo);
            }
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (type == SlotType.ModuleEquipSlot)
            {
                UnEquip();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (WorkBenchController.instance.canopenCustomize == false || (item == null || count < 1)
            || isEquiped)
            return;

        Customize.instance.fromSlot = this;
        icon.color = Color.gray;

        if (type == SlotType.ModuleEquipSlot)
        {
            CustomizeSlot fromSlot = Customize.instance.inventorySlots.Find(x => x.item.itemName == item.itemName);
            fromSlot.icon.color = Color.gray;
        }

        HandManager.instance.TakeItem(item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandManager.instance.UpdateHand();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Customize.instance.UpdateSlot();
        HandManager.instance.Drop();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //ÀåÂø½½·Ô ¿Ü ¹ÝÀÀX
        if (type != SlotType.ModuleEquipSlot)
            return;

        if (HandManager.instance.item != null)
        {
            CustomizeSlot fromSlot = Customize.instance.fromSlot;

            //ÀåÂø½½·Ô->ÀåÂø½½·Ô
            if (fromSlot.type == SlotType.ModuleEquipSlot)
            {
                Change(fromSlot, this);
            }

            //ÀÎº¥Åä¸®->ÀåÂø½½·Ô
            else
            {
                if (fromSlot.isEquiped)
                    return;

                Equip(HandManager.instance.item as Module);
            }
        }
    }

    public void Change(CustomizeSlot fromSlot, CustomizeSlot curSlot)
    {
        Item tmpItem = fromSlot.item;
        fromSlot.item = curSlot.item;
        curSlot.item = tmpItem;

        int tmpCount = fromSlot.count;
        fromSlot.count = curSlot.count;
        curSlot.count = tmpCount;

        Customize.instance.UpdateSlot();
    }

    public void Equip(Module module)
    {
        if (Customize.instance.occupancyPoint + module.occupancyPoint > 100)
            return;

        CustomizeSlot fromSlot = Customize.instance.inventorySlots.Find(x => x.item.itemName == module.itemName);
        fromSlot.isEquiped = true;

        item = module;
        count = 1;

        Customize.instance.UpdateSlot();
        PlayerController.instance.StateUpdate();
    }

    public void UnEquip()
    {
        CustomizeSlot fromSlot = Customize.instance.inventorySlots.Find(x => x.item.itemName == item.itemName);
        fromSlot.isEquiped = false;

        item = null;
        count = 0;

        Customize.instance.UpdateSlot();
        PlayerController.instance.StateUpdate();
    }

    public override void UpdateSlot()
    {
        if (count > 0)
        {
            icon.sprite = item.icon;

            if (isEquiped)
            {
                icon.color = Color.gray;
            }

            else
            {
                icon.color = Color.white;
            }
        }

        else
        {
            if (icon != null)
                icon.color = new Color(0, 0, 0, 0);
        }
    }
}
