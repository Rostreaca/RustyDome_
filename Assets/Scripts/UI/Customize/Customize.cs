using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour
{
    public static Customize instance;
    public Text informText;

    [SerializeField]
    private List<CustomizeSlot> slots = new List<CustomizeSlot>();

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
        slots = GetComponentsInChildren<CustomizeSlot>().ToList();

        foreach (Slot slot in slots)
        {
            slot.UpdateSlot();
        }
    }

    public void AddModule(Module module)
    {
        foreach (CustomizeSlot slot in slots)
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
}
