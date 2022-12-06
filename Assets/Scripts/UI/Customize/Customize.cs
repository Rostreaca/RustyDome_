using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Customize : MonoBehaviour, IPointerClickHandler
{
    public static Customize instance;

    public Text informText;
    public string basicText;
    public Text occupancyText;
    public Image occupancyImg;

    public int maxOccupancyPoint;
    public int curOccupancyPoint;

    public bool canCustomize;

    public List<CustomizeSlot> equipSlots = new List<CustomizeSlot>();
    public List<CustomizeSlot> inventorySlots = new List<CustomizeSlot>();

    public CustomizeSlot fromSlot;

    public Item testItem;

    void SIngleton_Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Awake()
    {
        SIngleton_Init();
    }

    private void Start()
    {
        UpdateSlot();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            DisplayInform(basicText);
        }
    }

    public void AddModule(Module module)
    {
        foreach (CustomizeSlot slot in inventorySlots)
        {
            if (slot.Add(module))
            {
                return;
            }
        }
    }

    public void DisplayInform(string text)
    {
        informText.text = text;
    }

    public void UpdateSlot()
    {
        foreach (CustomizeSlot slot in equipSlots)
        {
            slot.UpdateSlot();
        }

        foreach (CustomizeSlot slot in inventorySlots)
        {
            slot.UpdateSlot();
        }

        UpdateOccupancy();
    }

    public void UpdateOccupancy()
    {
        curOccupancyPoint = 0;

        foreach (CustomizeSlot slot in equipSlots)
        {
            if (slot.item != null)
            {
                Module module = slot.item as Module;
                curOccupancyPoint += module.occupancyPoint;
            }
        }

        occupancyImg.fillAmount = (float)curOccupancyPoint / (float)maxOccupancyPoint;
        occupancyText.text = curOccupancyPoint.ToString() + " / " + maxOccupancyPoint.ToString();
    }
}
