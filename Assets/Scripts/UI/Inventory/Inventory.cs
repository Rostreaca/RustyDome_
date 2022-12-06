using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Text informText;

    public List<InventorySlot> slots = new List<InventorySlot>();
    public Item testItem;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateSlot();
    }

    public void AddItem(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Add(item))
            {
                return;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Minus(item))
            {
                return;
            }
        }
    }

    public bool Search(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Ishave(item))
            {
                return true;
            }
        }
        return false;
    }

    public void DisplayInform(string text)
    {
        informText.text = text;

        Debug.Log(informText + "이게 고장났나?");
    }

    public void UpdateSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.UpdateSlot();
        }
    }
}
