using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackTrigger : MonoBehaviour
{
    public Combat combat;

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        combat.colliderDetected = collision;
        combat.HitDetected();
    }
}