using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public string slotItemName;
    public int count;

    public Image icon;

    public virtual void Start()
    {
        icon = transform.GetChild(0).GetComponent<Image>();

        if (item != null)
            slotItemName = item.itemName;
    }

    public abstract void OnPointerClick(PointerEventData eventData);

    public bool AddItem(Item newitem)
    {
        if (slotItemName == newitem.itemName)
        {
            if (count < item.stackSize)
            {
                // 아이템을 중첩
                count++;
                UpdateSlot();
                return true;
            }
        }
        return false;
    }

    public void RemoveItem()
    {
        count--;
        UpdateSlot();
    }

    public abstract void UpdateSlot();

}
