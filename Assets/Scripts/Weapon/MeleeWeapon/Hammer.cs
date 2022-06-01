using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hammer", menuName = "ScriptableObjects/MeleeWeapon/Hammer", order = 1)]
public class Hammer : MeleeWeapon
{
    public Hammer()
    {
        animName = "HammerAttack";
        itemName = "Hammer";
    }
}
