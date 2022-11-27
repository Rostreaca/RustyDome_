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
    public void OnRangeAttackEnd()
    {
        animator.SetBool("RangeAttack", false);
        enemyController.isRangeAttack = false;
    }

    public void CreateProjectile()
    {
        Instantiate(enemyController.Projectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, enemyController.Actor.transform);
    }

    public override void MeleeHitDetected()
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

    public void SoundPlay(AudioClip audio)
    {
        SoundManager.instance.SFXPlay("aa", audio);
    }
}
