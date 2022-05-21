using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    private Animator animator;
    private EnemyController enemyController;

    public override void Start()
    {
        base.Start();

        enemyController = GetComponentInParent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    public void OnMeleeAttackEnd()
    {
        animator.SetBool("MeleeAttack", false);

        enemyController.isAttack = false;
    }

    public override void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Player"))
        {
            PlayerController player = colliderDetected.GetComponent<PlayerController>();

            int damage = enemyController.meleeDmg;

            MeleeAttack(player, damage);
        }
    }

    public void MeleeAttack(PlayerController player, int damage)
    {
        player.GetDamage(damage, enemyController.transform);
    }
}
