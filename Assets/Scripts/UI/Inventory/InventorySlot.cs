﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices.WindowsRuntime;

public class InventorySlot : Slot, IPointerEnterHandler
{
    public InventorySlot equipSlot;
    public Text countText;

    public Scroll scroll;

    public bool isScroll;

    public enum SlotType
    {
        CoreEquipSlot,
        MeleeWeaponEquipSlot,
        RangeWeaponEquipSlot,
        SpecialWeaponEquipSlot,
        MobileWeaponEquipSlot,
        InventorySlot
    }
    public SlotType type;

    public override void Start()
    {
        base.Start();

        countText = GetComponentInChildren<Text>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (hasItem)
            {
                Inventory.instance.DisplayInform(item.itemInfo);
            }
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (hasItem && type == SlotType.InventorySlot)
            {
                equipSlot.item = item;
                equipSlot.slotItemName = item.itemName;
                equipSlot.count = 1;
                equipSlot.UpdateSlot();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isScroll)
        {
            scroll.isPointerOver = true;
        }
    }

    public override void UpdateSlot()
    {
        if (count > 0)
        {
            if (type != SlotType.InventorySlot)
            {
                if (slotItemName != null && item == null)
                {
                    item = Inventory.instance.inventorySlots.Find(x => x.HasItem(slotItemName)).item;
                }

                PlayerController player = PlayerController.instance;

                switch (type)
                {
                    case SlotType.CoreEquipSlot:
                        break;

                    case SlotType.MeleeWeaponEquipSlot:
                        player.meleeWeapon = item as MeleeWeapon;
                        break;

                    case SlotType.RangeWeaponEquipSlot:
                        player.rangeWeapon = item as RangeWeapon;
                        break;

                    case SlotType.SpecialWeaponEquipSlot:
                        player.specialWeapon = item as SpecialWeapon;
                        break;

                    case SlotType.MobileWeaponEquipSlot:
                        break;
                }
            }

            if (count > 1)
            {
                countText.text = count.ToString();
                countText.color = Color.black;
            }

            if (icon != null)
            {
                icon.sprite = item.icon;
                icon.color = Color.white;
            }
        }

        else
        {
            if (countText != null)
                countText.color = new Color(0, 0, 0, 0);
            
            if(icon !=null)
            icon.color = new Color(0, 0, 0, 0);
        }
    }

    public bool HasItem(string itemName)
    {
        if (!hasItem)
            return false;

        else
        {
            if (item.itemName == itemName)
                return true;

            else
                return false;
        }
    }
}
