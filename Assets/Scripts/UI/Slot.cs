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
    public bool hasItem
    {
        get
        {
            return item != null && count >= 1;
        }
    }

    public Image icon;

    
    private void OnDestroy()
    {
        Debug.Log("파괴가 되었니?");
    }
    public virtual void Start()
    {
        icon = transform.GetChild(0).GetComponent<Image>();

        if (item != null)
            slotItemName = item.itemName;

        UpdateSlot();
    }
    public abstract void OnPointerClick(PointerEventData eventData);

    public bool Add(Item newitem)
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

    public bool Minus(Item newitem)
    {
        if (slotItemName == newitem.itemName)
        {
            if (count == item.stackSize)
            {
                // 아이템을 차감
                count--;
                UpdateSlot();
                return true;
            }
        }
        return false;
    }

    public bool Ishave(Item newitem)
    {
        if (slotItemName == newitem.itemName)
        {
            if (count == item.stackSize)
            {
                // 아이템을 확인
                return true;
            }
        }
        return false;
    }

    public void Remove()
    {
        count--;
        UpdateSlot();
    }

    public abstract void UpdateSlot();

}
