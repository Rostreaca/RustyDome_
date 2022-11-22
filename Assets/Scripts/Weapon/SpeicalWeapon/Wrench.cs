using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wrench", menuName = "ScriptableObjects/SpecialWeapon/Wrench", order = 1)]
public class Wrench : SpecialWeapon
{
    public Wrench()
    {
        animName = "SpecialAttack";
        itemName = "Wrench";
    }
}