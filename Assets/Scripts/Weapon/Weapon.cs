using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public string animName;
    public int dmg;
    public int powerCon;

    public abstract void Use();

}
