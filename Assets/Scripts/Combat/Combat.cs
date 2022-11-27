using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Collider2D colliderDetected;
    private MeleeAttackTrigger meleeAttackTrigger;

    public virtual void Start()
    {
        meleeAttackTrigger = GetComponentInChildren<MeleeAttackTrigger>();
        meleeAttackTrigger.combat = this;
    }

    public virtual void MeleeHitDetected()
    {

    }

}
