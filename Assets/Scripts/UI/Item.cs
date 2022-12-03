using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite icon;
    public int stackSize;
    public string itemName;

    [TextArea(4, 10)]
    public string itemInfo;
}