using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Text informText;

    [TextArea(4, 10)]
    public string Testinfo;

    
    public List<InventorySlot> slots = new List<InventorySlot>();


    public Item testItem;

    void SIngleton_Init()
    {
        instance = this;
    }

    void Awake()
    {
    }
    private void Update()
    {
        if(GameObject.Find("InformText"))
        {
            informText = GameObject.Find("InformText").GetComponent<Text>();
        }
        slots = GetComponentsInChildren<InventorySlot>().ToList();
    }
    private void Start()
    {
        SIngleton_Init();
            

        foreach(InventorySlot slot in slots)
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
        Testinfo = text;
        informText.text = Testinfo;
    }
}
