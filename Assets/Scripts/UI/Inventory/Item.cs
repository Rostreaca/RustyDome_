using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField]
    private Sprite icon;

    public int stackSize;
    public string itemName;

    [TextArea]
    public string itemInfo;

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }
}
