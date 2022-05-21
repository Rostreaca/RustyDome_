using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern2Combat : MonoBehaviour
{
    public Collider2D colliderDetected;
    public int pattern2dmg = 60;


    public void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Player"))
        {
            PlayerController player = colliderDetected.GetComponent<PlayerController>();

            int damage = pattern2dmg;

            MeleeAttack(player, damage);
        }
    }

    public void MeleeAttack(PlayerController player, int damage)
    {
        player.GetDamage(damage, transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderDetected = collision;
        HitDetected();
    }
}
