using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    GameObject Actor;
    public GameObject projectile;
    public Transform projectileTransform;
    private Rigidbody2D rigid;
    private Animator animator;
    private PlayerController playerController;

    private bool canCombo;
    private bool comboReserve;

    public override void Start()
    {
        Actor = GameObject.Find("Actor");
        base.Start();

        rigid = GetComponentInParent<Rigidbody2D>();
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
        projectileTransform = transform.GetChild(2).transform;
    }

    public void ComboTrigger()
    {
        StartCoroutine(ICombo()); //Start combo system 
    }

    public void OnMeleeAttackBegin()
    {
        //Rotation
        if (Input.GetAxis("Horizontal") > 0)
            animator.SetBool("Flip", false);
        else
            animator.SetBool("Flip", true);
    }

    public void OnMeleeAttackEnd()
    {
        if (comboReserve)
        {
            animator.SetTrigger("AttackCombo");
            comboReserve = false;
        }

        else
        {
            StopCoroutine(ICombo()); //Stop combo 

            //Animator update
            animator.ResetTrigger("AttackCombo");
            animator.SetBool(playerController.meleeWeapon.animName, false);

            canCombo = false; //block combo
            playerController.isAttack = false;
        }
    }

    public void CreateProjectile()
    {
        Instantiate(projectile, projectileTransform.position, Quaternion.identity, Actor.transform);
    }

    public void OnRangeAttackEnd()
    {
        animator.SetBool("RangeAttack", false);

        playerController.isAttack = false;
    }

    IEnumerator ICombo()
    {
        canCombo = true;

        while (canCombo)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                canCombo = false;
                comboReserve = true;
                StopCoroutine(ICombo());
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
