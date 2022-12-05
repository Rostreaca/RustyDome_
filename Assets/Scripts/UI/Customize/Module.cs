using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Module", order = 1)]
public class Module : Item
{
    public int occupancyPoint;

    public int hp;
    public int power;
    public int powerRegen;
    public float meleeDmg;
    public float attackSpeed;
    public float moveSpeed;
}