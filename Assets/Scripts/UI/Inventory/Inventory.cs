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
    public GameObject[] slotbox;

    public Item testItem;

    void SIngleton_Init()
    {
        instance = this;
    }

    private void Update()
    {
        if(GameObject.Find("InformText"))
        {
            informText = GameObject.Find("InformText").GetComponent<Text>();
        }
        slotbox[1] = gameObject.GetComponent<GameObject>();
       slots = GetComponentsInChildren<InventorySlot>().ToList();
    }
    private void Start()
    {
        SIngleton_Init();

        Slot_Init();
    }

    public void Slot_Init()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.UpdateSlot();
        }
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
    }
}
