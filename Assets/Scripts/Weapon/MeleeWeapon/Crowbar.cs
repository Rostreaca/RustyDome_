using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crowbar", menuName = "ScriptableObjects/MeleeWeapon/Crowbar", order = 1)]
public class Crowbar : MeleeWeapon
{
    public Crowbar()
    {
        animName = "CrowbarAttack";
        itemName = "Crowbar";
    }
}