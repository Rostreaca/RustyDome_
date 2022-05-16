using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : Slot
{
    public InventorySlot equipSlot;
    public Text countText;

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
            if (equipSlot != null)
            {
                equipSlot.item = item;
                equipSlot.count = 1;
                equipSlot.UpdateSlot();

                if (equipSlot.gameObject.name == "MeleeWeaponEquipSlot")
                    PlayerController.instance.meleeWeapon = equipSlot.item as MeleeWeapon;
            }
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

            icon.sprite = item.icon;
            icon.color = Color.white;
        }

        else
        {
            countText.color = new Color(0, 0, 0, 0);
            icon.color = new Color(0, 0, 0, 0);
        }
    }
}
