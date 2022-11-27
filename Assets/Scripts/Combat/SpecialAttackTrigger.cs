using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTrigger : MonoBehaviour
{
    public PlayerCombat combat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        combat.colliderDetected = collision;
        combat.SpecialHitDetected();
    }
}