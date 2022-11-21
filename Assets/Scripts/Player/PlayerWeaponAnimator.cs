using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAnimator : MonoBehaviour
{
    public Animator playerAnimator, weaponAnimator;

    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack1"))
        {
            weaponAnimator.Play("MeleeAttack1");
        }
    }
}
