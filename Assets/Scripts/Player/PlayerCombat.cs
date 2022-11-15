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
    private bool isAiming;

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
            playerController.isMeleeAttack = false;
        }
    }

    public void OnMeleeChargeAttackEnd()
    {
        playerController.isMeleeAttack = false;
    }

    public void CreateProjectile()
    {
        Instantiate(projectile, projectileTransform.position, Quaternion.identity, Actor.transform);
    }

    public void OnRangeAttackAim()
    {
        if (animator.GetBool("RangeAttack"))
        {
            //사격가능
            if (playerController.powerNow >= playerController.rangeWeapon.powerCon &&
                playerController.ammoNow >= playerController.rangeWeapon.ammoCon)
            {
                playerController.powerNow -= playerController.rangeWeapon.powerCon;
                playerController.ammoNow -= playerController.rangeWeapon.ammoCon;

                animator.SetTrigger("RangeAttack_Shoot");
            }

            //사격불가
            else
            {
                animator.SetTrigger("RangeAttack_OutOfAmmo");
            }

            animator.SetBool("RangeAttack", false);
        }

        else
        {
            if (!isAiming)
            {
                isAiming = true;
                StartCoroutine(IAim());
            }
        }
    }

    private IEnumerator IAim()
    {
        float t = 0;
        while (t < 5)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.SetBool("RangeAttack", true);
                break;
            }
            t += Time.deltaTime;
            yield return null;
        }

        if (t >= 5)
        {
            animator.SetTrigger("RangeAttack_End");
        }

        isAiming = false;
        yield return null;
    }

    public void RangeAttack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, new Vector2(0, 0));
        if (hit.collider != null)
        {
            Collider2D col = hit.collider;
            if (col.gameObject.CompareTag("Enemy"))
            {
                EnemyController enemy = colliderDetected.GetComponent<EnemyController>();
                RangeAttack(enemy);
            }

            else if (col.gameObject.CompareTag("Boss"))
            {
                BossGetDamage boss = colliderDetected.GetComponent<BossGetDamage>();
                RangeAttacktoBoss(boss);
            }
        }
    }

    public void OnRangeAttackEnd()
    {
        playerController.isRangeAttack = false;
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
            MeleeAttack(enemy);
        }

        else if (colliderDetected.gameObject.CompareTag("Boss"))
        {
            BossGetDamage boss = colliderDetected.GetComponent<BossGetDamage>();
            MeleeAttacktoBoss(boss);
        }
    }

    public void MeleeAttack(EnemyController enemy)
    {
        enemy.GetDamage(playerController.meleeWeapon.dmg, playerController.meleeWeapon.stunDmg);
    }

    public void RangeAttack(EnemyController enemy)
    {
        enemy.GetDamage(playerController.rangeWeapon.dmg, playerController.rangeWeapon.stunDmg);
    }

    public void MeleeAttacktoBoss(BossGetDamage boss)
    {
        boss.GetDamage(playerController.meleeWeapon.dmg);
    }

    public void RangeAttacktoBoss(BossGetDamage boss)
    {
        boss.GetDamage(playerController.rangeWeapon.dmg);
    }

    public void SoundPlay(AudioClip audio)
    {

        SoundManager.instance.SFXPlay("aa", audio);
    }
}
