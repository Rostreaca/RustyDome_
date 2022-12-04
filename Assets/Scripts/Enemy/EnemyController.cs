using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{

    public int hpMax;
    public int hpNow;

    public float speed;

    public float stunMeterMax;
    public float stunMeterNow;
    public float stunMeterRegen;
    public float stunMeterRegenTime;
    public float stunMeterRegenTimer;
    public float stunTime = 1.5f;
    public float stunTimer = 0f;

    public int meleeDmg;
    public float rangeDmg;

    public float meleeSkillCoolTimeNow = 0f;
    public float meleeSkillCoolTimeMax = 2.0f;

    public float RangeSkillCoolTimeNow = 0f;
    public float RangeSkillCoolTimeMax = 3.0f;

    public float patrollRadius;
    public float patrollSin;
    public float returnRadius;
    public float attackRadius;
    public float rangeRadius;
    public Transform followTarget;

    public AudioClip[] sfxclip;

    public GameObject Coin;

    public GameObject GoldCoin;
    public GameObject SilverCoin;

    public GameObject Projectile;
    public GameObject Actor;

    public CapsuleCollider2D col;
    private SpriteRenderer sprite;
    public Animator animator;

    private Vector2 starterPos;
    public float patrollTimer = 0.02f;
    private float returnTimer = 3;

    [Header("상태")]
    public bool isAttack;
    public bool isRangeAttack;
    public bool cooltimecheck;
    public bool rangecoolcheck;
    private bool isMove;
    public bool isStun;
    public bool isDead;
    public bool isGround;
    public bool isPatrolling;
    public bool isFollowing;
    public bool isReturning;
    public bool _create_effect;
    public bool inSceneBound;
    public Collider2D checkcol;
    private string meleeAttackanim = "MeleeAttack";
    private string RangeAttackanim = "RangeAttack";
    
    private void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        isPatrolling = true;
        starterPos = transform.position;
        Actor = GameObject.Find("Actor");
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGame && !inSceneBound)
        {
            if (!isDead && !isStun)
            {
                Move();
            }
        }
        else
            animator.SetBool("Move", false);
    }

    private void Update()
    {
        if (!isDead)
        {
            if (GameManager.Instance.isGame &&!inSceneBound)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim) && !animator.GetCurrentAnimatorStateInfo(0).IsName(RangeAttackanim)
                    && !isStun)
                {
                    Rotation();
                }

                Stun_Regen();
                Condition();
                Animation();
                Attack_Stop();
            }
        }
    }

    public void Stun_Regen()
    {
        if (stunMeterNow < stunMeterMax)
        {
            if (stunMeterRegenTimer <= 0)
            {
                stunMeterNow += stunMeterRegen * Time.deltaTime;
            }
            else
            {
                stunMeterRegenTimer -= Time.deltaTime;
            }
        }
    }

    public void Move()
    {
        GroundCheck();

        if (isPatrolling && !isFollowing && !isReturning)
        {
            Patroll();
        }
    }

    private void Patroll()
    {
        isMove = true;

        patrollSin += patrollTimer * speed;

        if (patrollSin >= 2)
        {
            patrollTimer = -0.02f;
        }
        else if (patrollSin <= -2)
        {
            patrollTimer = 0.02f;
        }

        Cliffcheck();

        float x = patrollRadius * patrollSin + starterPos.x;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(x, transform.position.y), speed * Time.deltaTime);
    }

    public void Cliffcheck()
    {
        Vector2 frontcheck = new Vector2(transform.position.x + 0.5f, transform.position.y);

        if (sprite.flipX)
        {
            frontcheck.x = transform.position.x - 0.5f;
        }
        else
        {
            frontcheck.x = transform.position.x + 0.5f;
        }

        int layermask = ((1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Platform")));
        Debug.DrawRay(frontcheck, Vector3.down);
        RaycastHit2D cliffray = Physics2D.Raycast(frontcheck, Vector3.down, 1,layermask);

        checkcol = cliffray.collider ;

        if (cliffray.collider == null)
        {
            patrollTimer = -patrollTimer;
        }
    }

    public void GroundCheck()
    {
        int layermask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, col.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, layermask);

        if (hit.collider != null && hit.collider.tag == ("NPC") || hit.collider != null && hit.collider.tag == ("Projectile"))
        {
            return;
        }

        bool grounded = (hit.collider != null && hit.collider.CompareTag("Ground")) || (hit.collider != null && hit.collider.CompareTag("Platform")) || (hit.collider != null && hit.collider.tag == ("Quest"));
        isGround = grounded;

        if (!isGround)
            patrollTimer = -patrollTimer;
    }

    public void Follow(Transform target)
    {
        followTarget = target;

        if (!isFollowing && !isReturning && !isStun)
        {
            isFollowing = true;
            StartCoroutine(IFollow());
        }
    }

    public void Condition() //상태 설정
    {
        if (isFollowing || isReturning)
        {
            isPatrolling = false;
        }
        else if (!isFollowing && animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim))
        {
            isPatrolling = false;
        }
        else
            isPatrolling = true;

        if (isReturning)
            isPatrolling = false;

        if (!isFollowing && !isReturning && !isPatrolling && !animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim))
        {
            isPatrolling = true;
        }
    }

    private void Rotation()
    {
        //방향전환
        if (isPatrolling)
        {
            if (patrollTimer > 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        if (isFollowing)
        {
            if (transform.position.x <= followTarget.position.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        if (isReturning)
        {
            if (transform.position.x <= starterPos.x)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }
    }

    private void Attack()
    {
        if (meleeSkillCoolTimeNow <= 0)
        {
            RangeSkillCoolTimeNow = 2;
            rangecoolcheck = false;
            cooltimecheck = false;
            isMove = false;
            isAttack = true;
            animator.SetTrigger("MeleeAttack");
        }
    }
    
    private void RangeAttack()
    {
        if (RangeSkillCoolTimeNow <= 0)
        {
            meleeSkillCoolTimeNow = 1.5f;
            cooltimecheck = false;
            rangecoolcheck = false;
            isMove = false;
            isRangeAttack = true;
            animator.SetTrigger("RangeAttack");
        }
    }

    public void Attack_Stop()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim))
        {
            isRangeAttack = false;
            isAttack = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim))
        {
            isMove = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(RangeAttackanim))
        {
            isMove = false;
        }

        if (cooltimecheck == false && !animator.GetCurrentAnimatorStateInfo(0).IsName(RangeAttackanim))
        {
            meleeSkillCoolTimeNow += Time.deltaTime;
        }
        if (meleeSkillCoolTimeNow >= meleeSkillCoolTimeMax)
        {
            meleeSkillCoolTimeNow = 0f;
            cooltimecheck = true;
        }

        if (rangecoolcheck == false && !animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim))
        {
            RangeSkillCoolTimeNow += Time.deltaTime;
        }
        if (RangeSkillCoolTimeNow >= RangeSkillCoolTimeMax)
        {
            RangeSkillCoolTimeNow = 0f;
            rangecoolcheck = true;
        }

    } // 공격 쿨타임

    public void GetDamage(int damage, int stunDamage)
    {
        isMove = false;
        if (isDead != true)
        {
            isFollowing = false;
            hpNow -= damage;
            stunMeterNow -= stunDamage;
            _create_effect = true;
            animator.SetTrigger("Hit");
            stunMeterRegenTimer = stunMeterRegenTime;

            StopAllCoroutines();

            if (stunMeterNow <= 0)
            {
                Stun();
            }

            if (hpNow <= 0)
            {
                Death();
            }
        }
    }

    public void Stun()
    {
        isStun = true;
        animator.SetBool("Stun", true);
        stunTimer = stunTime;
        StartCoroutine(IStun());
    }

    IEnumerator IStun()
    {
        while (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            yield return null;
        }

        isStun = false;
        animator.SetBool("Stun", false);
        stunMeterNow = stunMeterMax;
        yield return null;
    }

    public void Death()
    {
        if(GameManager.Instance.isQuestStart == true&& SceneManage.Instance.nowscene.buildIndex == 5)
        {
            QuestManager.instance.Enemycount++;
        }
        if (SceneManage.Instance.nowscene.buildIndex == 2)
        {
            QuestManager.instance.Scene2enemycount--;
        }
        transform.position = transform.position;
        Rigidbody2D rigid;
        rigid = GetComponent<Rigidbody2D>();

        rigid.isKinematic = true;
        col.enabled = false;

        animator.SetTrigger("Death");
        isDead = true;

        Instantiate(Coin, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, Actor.transform);

        int silver = Random.Range(0, 10);
        Debug.Log("silver"+silver);
        int gold = Random.Range(0, 10);
        Debug.Log("gold"+gold);
        
        for (int j = 1; j <= silver; j++)
        {
            float dropPos = Random.Range(-0.5f, 0.5f);
            
            Instantiate(SilverCoin, new Vector2(transform.position.x + dropPos, transform.position.y + 0.5f), Quaternion.identity, Actor.transform);
        }
        for (int j = 1; j <= gold; j++)
        {
            float dropPos = Random.Range(0, 0);

            Instantiate(GoldCoin, new Vector2(transform.position.x + dropPos, transform.position.y + 0.5f), Quaternion.identity, Actor.transform);
        }
    }

    public void Animation()
    {
        animator.SetBool("Move", isMove);
    }


    IEnumerator IFollow()
    {
        float timer = returnTimer; //timer to return after palyer leave

        if (!isDead && !isStun)
        {
            while (isFollowing)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim) || animator.GetCurrentAnimatorStateInfo(0).IsName(RangeAttackanim))
                {
                    isPatrolling = false;
                    isReturning = false;
                    isMove = false;
                    yield return null;
                }

                isPatrolling = false;
                if (Vector2.Distance(transform.position, followTarget.position) > returnRadius && timer <= 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim) && !animator.GetCurrentAnimatorStateInfo(0).IsName(RangeAttackanim)) //if target leave
                {
                    isFollowing = false;
                    followTarget = null;

                    isReturning = true;

                    patrollSin = 0;
                    if (sprite.flipX)
                    {
                        patrollTimer = -patrollTimer;
                    }

                    StartCoroutine(IReturnToStartPos()); //start returning
                }
                else if (!isGround) //if the target jumps over the gap  
                {
                    isFollowing = false;
                    followTarget = null;

                    isReturning = true;

                    if (sprite.flipX)
                    {
                        patrollTimer *= -patrollTimer;
                    }
                    patrollSin = 0;

                    StartCoroutine(IReturnToStartPos());
                }
                else
                {
                    if (Vector2.Distance(transform.position, followTarget.position) > rangeRadius && !isAttack && !isRangeAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim) && !animator.GetCurrentAnimatorStateInfo(0).IsName(RangeAttackanim))
                    {
                        isMove = true;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(followTarget.position.x, transform.position.y), speed * Time.deltaTime);

                    }
                    else if (isRangeAttack == false && Vector2.Distance(transform.position, followTarget.position) < rangeRadius && Vector2.Distance(transform.position, followTarget.position) > attackRadius)
                    {
                        RangeAttack(); //attack
                    }
                    else if (isAttack == false && Vector2.Distance(transform.position, followTarget.position) < attackRadius)
                    {
                        Attack(); //attack
                    }

                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName(meleeAttackanim) && !animator.GetCurrentAnimatorStateInfo(0).IsName(RangeAttackanim) && Vector2.Distance(transform.position, followTarget.position) < rangeRadius && Vector2.Distance(transform.position, followTarget.position) > attackRadius)
                    {
                        isMove = true;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(followTarget.position.x, transform.position.y), speed * Time.deltaTime);
                    }

                    timer -= Time.deltaTime;
                }

                yield return null;
            }
        }

        yield return null;
    }

    IEnumerator IReturnToStartPos()
    {
        if (!isDead && !isStun)
        {
            if(isFollowing||isAttack||isRangeAttack||isPatrolling)
            {
                yield return null;
            }
            while (transform.position.x != starterPos.x)
            {
                isMove = true;

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(starterPos.x, transform.position.y), speed * Time.deltaTime); //move to start pos

                yield return null;
            }

            isReturning = false;
            yield return null;
        }
    }
    public bool wascounted = false;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Quest" && !wascounted)
        {
            if (SceneManage.Instance.nowscene.buildIndex == 2)
            {
                QuestManager.instance.Scene2enemycount++;
                wascounted = true;
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.DrawWireSphere(transform.position, returnRadius);
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }

}
