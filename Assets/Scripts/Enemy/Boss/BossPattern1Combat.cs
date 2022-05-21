using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern1Combat : MonoBehaviour
{
    public Collider2D colliderDetected;
    public int pattern1dmg = 20;


    public void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Player"))
        {
            PlayerController player = colliderDetected.GetComponent<PlayerController>();

            int damage = pattern1dmg;

            MeleeAttack(player, damage);
        }
    }

    public void MeleeAttack(PlayerController player, int damage)
    {
        player.GetDamage(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderDetected = collision;
        HitDetected();
    }
}
