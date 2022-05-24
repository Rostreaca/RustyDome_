using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    GameObject Actor;
    public GameObject projectile;
    private Rigidbody2D rigid;
    private Animator animator;
    private PlayerController playerController;

    private bool canCombo;

    public override void Start()
    {
        Actor = GameObject.Find("Actor");
        base.Start();

        rigid = GetComponentInParent<Rigidbody2D>();
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    public void OnMeleeAttackBegin(float timeToCombo)
    {
        StartCoroutine(ICombo(timeToCombo)); //Start combo system 

        //Rotation
        if (Input.GetAxis("Horizontal") > 0)
            animator.SetBool("Flip", false);
        else
            animator.SetBool("Flip", true);
    }

    public void OnMeleeAttackEnd()
    {
        StopCoroutine(ICombo(0)); //Stop combo 

        //Animator update
        animator.ResetTrigger("AttackCombo");
        animator.SetBool(playerController.meleeWeapon.animName, false);

        canCombo = false; //block combo
        playerController.isAttack = false;
    }

    public void CreateLeftProjectile()
    {
        Instantiate(projectile, new Vector2(transform.position.x - 0.75f, transform.position.y-0.28f), Quaternion.identity, Actor.transform);
        PlayerProjectile.instance.rigid.velocity = new Vector2(-PlayerProjectile.instance.speed, 0);
    }

    public void CreateRightProjectile()
    {
        Instantiate(projectile, new Vector2(transform.position.x+0.85f, transform.position.y-0.28f), Quaternion.identity, Actor.transform);
        PlayerProjectile.instance.rigid.velocity = new Vector2(PlayerProjectile.instance.speed, 0);
    }
    public void OnRangeAttackEnd()
    {
        animator.SetBool("RangeAttack", false);

        playerController.isAttack = false;
    }

    IEnumerator ICombo(float comboTimer)
    {
        canCombo = false;
        yield return new WaitForSeconds(comboTimer);
        canCombo = true;

        while (canCombo)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                canCombo = false;
                animator.SetTrigger("AttackCombo");
                StopCoroutine(ICombo(0));
            }
            yield return null;
        }
    }

    public override void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = colliderDetected.GetComponent<EnemyController>();

            int damage = playerController.meleeWeapon.dmg;

            MeleeAttack(enemy, damage);
        }
        else if (colliderDetected.gameObject.CompareTag("Boss"))
        {
            BossGetDamage boss = colliderDetected.GetComponent<BossGetDamage>();

            int damage = playerController.meleeWeapon.dmg;

            MeleeAttacktoBoss(boss, damage);
        }
    }
    public void MeleeAttack(EnemyController enemy, int damage)
    {
        enemy.GetDamage(damage);
    }

    public void MeleeAttacktoBoss(BossGetDamage boss, int damage)
    {
        boss.GetDamage(damage);
    }
}
