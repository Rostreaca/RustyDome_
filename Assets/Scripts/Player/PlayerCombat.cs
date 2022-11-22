using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    GameObject Actor;
    public GameObject projectile;
    public Transform projectileTransform;

    public Animator animator, weaponAnimator, rangeEffectAnimator;
    private Rigidbody2D rigid;
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
            animator.SetBool(playerController.meleeWeapon.animName, false);

            canCombo = false; //block combo
            playerController.isMeleeAttack = false;
        }
    }

    public void OnMeleeChargeAttackEnd()
    {
        playerController.isMeleeAttack = false;
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
            if (playerController.powerNow >= playerController.rangeWeapon.powerCon &&
                playerController.ammoNow >= playerController.rangeWeapon.ammoCon)
            {
                playerController.powerNow -= playerController.rangeWeapon.powerCon;
                playerController.ammoNow -= playerController.rangeWeapon.ammoCon;

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
        while (t < 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.X))
            {
                animator.SetBool("RangeAttack", true);
                break;
            }
            t += Time.deltaTime;
            yield return null;
        }

        if (t >= 1)
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
        playerController.isRangeAttack = false;
    }

    public void OnSpecialAttackEnd()
    {
        animator.SetBool(playerController.specialWeapon.animName, false);
        playerController.isSpecialAttack = false;
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

    public void OnRollBegin()
    {
        playerController.gameObject.layer = LayerMask.NameToLayer("RollingPlayer");
    }

    public void OnRollEnd()
    {
        playerController.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void SoundPlay(AudioClip audio)
    {

        SoundManager.instance.SFXPlay("aa", audio);
    }
}
