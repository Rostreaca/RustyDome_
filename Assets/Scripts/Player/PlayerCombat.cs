using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCombat : Combat
{
    public GameObject Actor;
    public GameObject projectile;
    public Transform projectileTransform;

    private SpecialAttackTrigger specialAttackTrigger;

    public Animator animator, weaponAnimator, rangeEffectAnimator;
    private PlayerController player;

    public EnemyController executionTarget;

    //public float chargeTime = 1f;
    public float aimingTime = 1f;
    public float excutionOffset;

    private bool canCombo;
    private bool comboReserve;
    private bool isAiming;


    public override void Start()
    {
        base.Start();
        specialAttackTrigger = GetComponentInChildren<SpecialAttackTrigger>();
        specialAttackTrigger.combat = this;

        Actor = GameObject.Find("Actor");
        player = GetComponentInParent<PlayerController>();
    }

    public void OnMeleeAttackBegin()
    {
        StartCoroutine(ICombo()); //Start combo system 
        //StartCoroutine(ICharge());
    }

    public void OnMeleeAttackEnd()
    {
        if (comboReserve)
        {
            animator.SetTrigger("AttackCombo");
            weaponAnimator.SetTrigger("AttackCombo");
            comboReserve = false;
        }

        else
        {
            StopCoroutine(ICombo()); //Stop combo 

            //Animator update
            animator.ResetTrigger("AttackCombo");
            weaponAnimator.ResetTrigger("AttackCombo");
            animator.SetBool(player.meleeWeapon.animName, false);

            canCombo = false; //block combo
            player.isMeleeAttack = false;
        }
    }

    public void OnMeleeChargeAttackEnd()
    {
        player.isMeleeAttack = false;
    }

    IEnumerator ICombo()
    {
        yield return new WaitForEndOfFrame();
        canCombo = true;

        while (canCombo)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Z))
            {
                canCombo = false;
                comboReserve = true;
            }
            yield return null;
        }
    }

    //public void OnMeleeChargeAttackBegin()
    //{
    //    playerController.isCharge = false;
    //}

    //IEnumerator ICharge()
    //{
    //    float time = 0;

    //    while (!playerController.isCharge)
    //    {
    //        if (Input.GetKeyUp(KeyCode.Mouse0))
    //        {
    //            StopCoroutine(ICharge());
    //        }

    //        if (time >= chargeTime)
    //        {
    //            playerController.isCharge = true;
    //        }

    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    public void CreateProjectile()
    {
        Instantiate(projectile, projectileTransform.position, Quaternion.identity, Actor.transform);
    }

    public void OnRangeAttackAim()
    {
        if (animator.GetBool("RangeAttack"))
        {
            //사격가능
            if (player.powerNow >= player.rangeWeapon.powerCon &&
                player.ammoNow >= player.rangeWeapon.ammoCon)
            {
                player.powerNow -= player.rangeWeapon.powerCon;
                player.ammoNow -= player.rangeWeapon.ammoCon;

                animator.SetTrigger("RangeAttack_Shoot");
                rangeEffectAnimator.SetTrigger("RangeAttack_Effect" + Random.Range(0, 3).ToString());
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
        while (t < aimingTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.X))
            {
                animator.SetBool("RangeAttack", true);
                break;
            }
            t += Time.deltaTime;
            yield return null;
        }

        if (t >= aimingTime)
        {
            animator.SetTrigger("RangeAttack_End");
        }

        isAiming = false;
        yield return null;
    }

    public void RangeAttack()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Enemy")) + (1 << LayerMask.NameToLayer("Ground"));
        float maxDistance = 7.5f;

        Vector2 startPos = new Vector3(transform.position.x, transform.position.y - 0.5f);
        Vector2 dir = new Vector3(gameObject.GetComponent<SpriteRenderer>().flipX ? -1 : 1, 0);

        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(1, 0.95f), 0, dir, maxDistance, layerMask);
        if (hit.collider != null)
        {
            Collider2D col = hit.collider;

            if (col.gameObject.CompareTag("Enemy"))
            {
                EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
                RangeAttack(enemy);
            }

            else if (col.gameObject.CompareTag("Boss"))
            {
                BossGetDamage boss = col.gameObject.GetComponent<BossGetDamage>();
                RangeAttacktoBoss(boss);
            }

            else if (col.gameObject.CompareTag("BreakableWall"))
            {
                col.GetComponent<BreakableWall>().Damage();
            }
        }
    }

    public void OnDrawGizmos()
    {
        int layerMask = (1 << LayerMask.NameToLayer("Enemy")) + (1 << LayerMask.NameToLayer("Ground"));
        float maxDistance = 7.5f;

        Vector3 startPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Vector3 dir = new Vector3(gameObject.GetComponent<SpriteRenderer>().flipX ? -1 : 1, 0);

        RaycastHit2D hit = Physics2D.BoxCast(startPos, new Vector2(1, 0.95f), 0, dir, maxDistance, layerMask);
        Gizmos.color = Color.red;

        if (hit)
        {
            Gizmos.DrawRay(startPos, dir * hit.distance);
            Gizmos.DrawWireCube(startPos + dir * hit.distance, new Vector2(1, 1));
        }
        else
        {
            Gizmos.DrawRay(startPos, dir * maxDistance);
        }
    }

    public void OnRangeAttackEnd()
    {
        player.isRangeAttack = false;
    }

    public void OnSpecialAttackEnd()
    {
        animator.SetBool(player.specialWeapon.animName, false);
        player.isSpecialAttack = false;
    }

    public override void MeleeHitDetected()
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

        else if (colliderDetected.gameObject.CompareTag("BreakableWall"))
        {
            colliderDetected.GetComponent<BreakableWall>().Damage();
        }
    }

    public void SpecialHitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = colliderDetected.GetComponent<EnemyController>();
            SpecialAttack(enemy);
        }
    }

    public void MeleeAttack(EnemyController enemy)
    {
        enemy.GetDamage(player.meleeWeapon.dmg, player.meleeWeapon.stunDmg);
    }

    public void RangeAttack(EnemyController enemy)
    {
        enemy.GetDamage(player.rangeWeapon.dmg, player.rangeWeapon.stunDmg);
    }

    public void MeleeAttacktoBoss(BossGetDamage boss)
    {
        boss.GetDamage(player.meleeWeapon.dmg);
    }

    public void RangeAttacktoBoss(BossGetDamage boss)
    {
        boss.GetDamage(player.rangeWeapon.dmg);
    }

    public void SpecialAttack(EnemyController enemy)
    {
        if (enemy.isStun)
        {
            Execution(enemy);
        }
    }

    public void Execution(EnemyController enemy)
    {
        player.isInvincible = true;
        player.gameObject.layer = LayerMask.NameToLayer("InvinciblePlayer");

        float offset = (enemy.col.size.x / 2) + excutionOffset;
        offset *= enemy.transform.position.x - player.transform.position.x > 0 ? -1 : 1;

        player.transform.position = new Vector3(enemy.transform.position.x + offset, transform.position.y);

        executionTarget = enemy;
        animator.SetTrigger("Execution");
        weaponAnimator.SetTrigger("Execution");
    }

    public void OnExecutionEnd()
    {
        executionTarget.GetDamage(executionTarget.hpMax, 0);

        player.isInvincible = false;
        player.gameObject.layer = LayerMask.NameToLayer("Player");

        animator.SetBool(player.specialWeapon.animName, false);
        player.isSpecialAttack = false;
    }

    public void Hit()
    {
        StopAllCoroutines();
    }

    public void OnRollBegin()
    {
        player.isInvincible = true;
        player.gameObject.layer = LayerMask.NameToLayer("InvinciblePlayer");
    }

    public void OnRollEnd()
    {
        player.isInvincible = false;
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void SoundPlay(AudioClip audio)
    {

        SoundManager.instance.SFXPlay("aa", audio);
    }
}
