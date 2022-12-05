using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
            if (item != null && count > 0)
                Inventory.instance.DisplayInform(item.itemInfo);
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (hasItem && type == SlotType.InventorySlot)
            {
                equipSlot.item = item;
                equipSlot.count = 1;
                equipSlot.UpdateSlot();

                PlayerController player = PlayerController.instance;

                switch (equipSlot.type)
                {
                    case SlotType.CoreEquipSlot:
                        break;

                    case SlotType.MeleeWeaponEquipSlot:
                        player.meleeWeapon = equipSlot.item as MeleeWeapon;
                        break;

                    case SlotType.RangeWeaponEquipSlot:
                        player.rangeWeapon = equipSlot.item as RangeWeapon;
                        break;

                    case SlotType.SpecialWeaponEquipSlot:
                        player.specialWeapon = equipSlot.item as SpecialWeapon;
                        break;

                    case SlotType.MobileWeaponEquipSlot:
                        break;

                    case SlotType.InventorySlot:
                        break;
                }
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
}
