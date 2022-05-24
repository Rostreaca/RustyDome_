using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public Text informText;

    [SerializeField]
    private List<InventorySlot> slots = new List<InventorySlot>();

    public Item testItem;

    /*
     * 인벤토리 UI내 슬롯 총 44개
     * 장비슬롯중 첫 슬롯은 Equip슬롯
     * 0~2 코어슬롯
     * 3~6 근접무기슬롯
     * 7~10 원거리무기슬롯
     * 11~14 특수무기슬롯
     * 15~18 모빌슬롯
     * 19~28 아이템슬롯
     * 29~43 특수아이템슬롯
     * 하이라키내에서 순서바꾸면 이것도 바뀜
     */

    void SIngleton_Init()
    {
        instance = this;
    }

    void Awake()
    {
        SIngleton_Init();
    }

    private void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>().ToList();

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

    public void DisplayInform(string text)
    {
        informText.text = text;
    }
}
