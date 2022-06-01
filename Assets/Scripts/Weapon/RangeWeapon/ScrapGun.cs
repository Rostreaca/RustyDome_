using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrapGun", menuName = "ScriptableObjects/RangeWeapon/ScrapGun", order = 1)]
public class ScrapGun : RangeWeapon
{
    public ScrapGun()
    {
        animName = "RangeAttack";
        itemName = "ScrapGun";
    }
}
