using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();
    //아이템의 아이콘
    public Image icon;
    public Text stackText;


    private void Start()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        stackText = GetComponentInChildren<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //클릭시
    }

    public Item Item
    {
        get
        {
            return items.Peek();
        }
    }


    public int Count
    {
        get
        {
            return items.Count;
        }
    }

    // 빈 슬롯 여부
    public bool IsEmpty
    {
        get { return items.Count == 0; }
    }


    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == Item.GetType())
        {
            int count = newItems.Count;
            for (int i = 0; i < count; i++)
            {
                // 아이템을 추가하고 newItems의 리스트에서 삭제합니다.
                AddItem(newItems.Pop());
            }
            return true;
        }
        return false;
    }

    // 슬롯에 아이템 추가.
    public bool AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.Icon;
        icon.color = Color.white;
        item.Slot = this;
        return true;
    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && (item.name == Item.name))
        {
            if (items.Count < Item.StackSize)
            {
                // 아이템을 중첩시킵니다.
                items.Push(item);
                item.Slot = this;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem()
    {
        if (!IsEmpty)
        {
            items.Pop();
        }
    }
}
