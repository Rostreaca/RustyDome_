using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLeftAttackTrigger : MonoBehaviour
{
    public Combat combat;

    private void Start()
    {

        combat = GetComponentInParent<Combat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        combat.colliderDetected = collision;
        combat.HitDetected();
    }
}
