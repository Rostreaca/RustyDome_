using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour
{
    public static Customize instance;

    public Text informText;
    public Text occupancyText;
    public Image occupancyImg;

    public int occupancyPoint;

    [SerializeField]
    private List<CustomizeSlot> equipSlots = new List<CustomizeSlot>();
    [SerializeField]
    private List<CustomizeSlot> inventorySlots = new List<CustomizeSlot>();

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
        equipSlots = transform.GetChild(0).GetComponentsInChildren<CustomizeSlot>().ToList();
        inventorySlots = transform.GetChild(1).GetComponentsInChildren<CustomizeSlot>().ToList();
        informText = transform.GetChild(2).GetComponentInChildren<Text>();
        occupancyText = transform.GetChild(3).GetComponentInChildren<Text>();
        occupancyImg = transform.GetChild(3).GetChild(1).GetComponent<Image>();

        UpdateOccupancy();
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

    public void UpdateOccupancy()
    {
        float occup = 0;

        foreach (CustomizeSlot slot in equipSlots)
        {
            if (slot.item != null)
            {
                Module module = slot.item as Module;
                occup += module.occupancyPoint;
            }
        }

        occupancyImg.fillAmount = occup / occupancyPoint;
        occupancyText.text = occup.ToString() + " / " + occupancyPoint.ToString();
    }
}
