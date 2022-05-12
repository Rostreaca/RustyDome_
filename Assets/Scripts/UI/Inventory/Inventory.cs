using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    private List<Slot> slots = new List<Slot>();
    private Slot fromSlot;

    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Inventory>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Start()
    {
        for(int i=0; i<40; i++)
        {
            slots.Add(GameObject.Find("InventorySlot (" + (i+1) + ")").GetComponent<Slot>());
        }
    }

    public Slot FromSlot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;
        }
    }

    public void Additem(Item item)
    {
        if(item.StackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return;
            }
        }
        PlaceInEmpty(item);
    }

    public bool PlaceInEmpty(Item item)
    {
        foreach (Slot slot in slots)
        {
            // 빈 슬롯이 있으면
            if (slot.IsEmpty)
            {
                // 해당 슬롯에 아이템을 추가한다.
                slot.AddItem(item);
                return true;
            }
        }
        return false;
    }

    public bool PlaceInStack(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.StackItem(item))
            {
                return true;
            }
        }
        return false;
    }
}
