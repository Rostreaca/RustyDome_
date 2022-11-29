using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTrigger : MonoBehaviour
{
    public Combat combat;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        combat.colliderDetected = collider;
        combat.MeleeHitDetected();
    }

}