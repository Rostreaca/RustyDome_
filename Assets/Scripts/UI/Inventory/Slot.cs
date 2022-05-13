using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public string slotItemName;
    public int count;

    public Image icon;
    public Text countText;

    private void Start()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        countText = GetComponentInChildren<Text>();

        if (item != null)
            slotItemName = item.itemName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
            Inventory.instance.DisplayInform(item.itemInfo);
    }

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

    public void UpdateSlot()
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
