using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int stackSize;
    public string itemName;

    [TextArea]
    public string itemInfo;

    private Slot slot;

    public Slot Slot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public int StackSize
    {
        get
        {
            return stackSize;
        }
    }

    public void Remove()
    {
        if(Slot != null)
        {
            Slot.RemoveItem();
        }
    }


}
