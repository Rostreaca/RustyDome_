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

    public float chargeTime = 1f;

    private bool canCombo;
    public bool comboReserve;

    public override void Start()
    {
        base.Start();
        Actor = GameObject.Find("Actor");
        rigid = GetComponentInParent<Rigidbody2D>();
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
        projectileTransform = transform.GetChild(2).transform;
    }

    public void OnMeleeAttackBegin()
    {
        StartCoroutine(ICombo()); //Start combo system 
        StartCoroutine(ICharge());
    }

    public void OnMeleeChargeAttackBegin()
    {
        playerController.isCharge = false;
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

    public void OnMeleeChargeAttackEnd()
    {
        playerController.isAttack = false;
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
            }
            yield return null;
        }
    }

    IEnumerator ICharge()
    {
        float time = 0;

        while (!playerController.isCharge)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                StopCoroutine(ICharge());
            }

            if (time >= chargeTime)
            {
                playerController.isCharge = true;
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    public override void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = colliderDetected.GetComponent<EnemyController>();

            int damage = playerController.meleeWeapon.dmg;
            int stunDamage = playerController.meleeWeapon.stunDmg;
            MeleeAttack(enemy, damage, stunDamage);
        }

        else if (colliderDetected.gameObject.CompareTag("Boss"))
        {
            BossGetDamage boss = colliderDetected.GetComponent<BossGetDamage>();

            int damage = playerController.meleeWeapon.dmg;

            MeleeAttacktoBoss(boss, damage);
        }
    }

    public void MeleeAttack(EnemyController enemy, int damage, int stunDamage)
    {
        enemy.GetDamage(damage, stunDamage);
    }

    public void MeleeAttacktoBoss(BossGetDamage boss, int damage)
    {
        boss.GetDamage(damage);
    }

    public void SoundPlay(AudioClip audio)
    {

        SoundManager.instance.SFXPlay("aa", audio);
    }
}
