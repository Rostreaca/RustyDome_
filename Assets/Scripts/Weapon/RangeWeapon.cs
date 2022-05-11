using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon", menuName = "ScriptableObjects/RangeWeapon", order = 1)]
public class RangeWeapon : Weapon
{
    public string animName;

    public int ammoMax;
    public int ammoNow;
    public int powerCon;
    public int ammoCon;
    public int skillDmg;
    public int skillPowerCon;
    public int skillAmmoCon;

    public override void Use()
    {

    }

    public void SpecialUse()
    {

    }
}
