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
    public GameObject Coin;
    public GameObject Projectile;
    public GameObject Actor;

    public bool isAttack;
    public bool isRangeAttack;
    public bool cooltimecheck;
    public bool rangecoolcheck;

    private CapsuleCollider2D col;
    private SpriteRenderer rend;
    private Animator animator;

    private Vector2 starterPos;
    private float patrollTimer = 0.02f;
    private float returnTimer = 3;
    private bool canMove;
    private bool isMove;
    private bool isGround;
    private bool isPatrolling;
    private bool isFollowing;
    private bool isReturning;

    private void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        canMove = true;
        isPatrolling = true;
        starterPos = transform.position;
        Actor = GameObject.Find("Actor");
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGame)
        {
            if (!canMove)
                return;

            Move();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isGame)
        {
            if (!canMove)
                return;
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("BrokenClockwoker_Melee_Right_Animation")|| !animator.GetCurrentAnimatorStateInfo(0).IsName("BrokenClockwoker_Range_Right_Ani"))
                Rotation();

            Animation();
            Attack_Stop();
        }
    }

    public void Move()
    {
        GroundCheck();

        //if (!isAttack)
        //    isMove = true;
        

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

        transform.position = Vector2.MoveTowards(transform.position,  new Vector2(x, transform.position.y), speed * Time.deltaTime);
    }
    public void Cliffcheck()
    {
        Vector2 frontcheck = new Vector2(transform.position.x + 0.5f, transform.position.y);
        if (transform.localScale == new Vector3(-1, 1, 1))
        {
            frontcheck.x = transform.position.x - 0.5f;
        }
        else
        {
            frontcheck.x = transform.position.x + 0.5f;
        }

        Debug.DrawRay(frontcheck, Vector3.down);
        RaycastHit2D cliffray = Physics2D.Raycast(frontcheck, Vector3.down, 1);

        if (cliffray.collider == null)
        {
            patrollTimer *= -1;
        }

    }
    public void Follow(Transform target)
    {
        followTarget = target;

        if (!isFollowing)
        {
            isFollowing = true;
            StartCoroutine(IFollow());
        }
    }

    private void Rotation()
    {
        if (isPatrolling)
        {
            if (patrollTimer > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (isFollowing)
        {
            if (transform.position.x <= followTarget.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if (isReturning)
        {
            if (transform.position.x <= starterPos.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void Attack()
    {
        if (meleeSkillCoolTimeNow <= 0)
        {
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
            rangecoolcheck = false;
            isMove = false;
            isRangeAttack = true;
            animator.SetTrigger("RangeAttack");
        }
    }
    public void Attack_Stop()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BrokenClockwoker_Melee_Right_Animation"))
        {
            isRangeAttack = false;
            isAttack = false;
        }

        if (cooltimecheck == false)
        {
            meleeSkillCoolTimeNow += Time.deltaTime;
        }
        if(meleeSkillCoolTimeNow >= meleeSkillCoolTimeMax || isMove == true )
        {
            meleeSkillCoolTimeNow = 0f;
            cooltimecheck = true;
        }

        if (rangecoolcheck == false)
        {
            RangeSkillCoolTimeNow += Time.deltaTime;
        }
        if (RangeSkillCoolTimeNow >= RangeSkillCoolTimeMax)
        {
            RangeSkillCoolTimeNow = 0f;
            rangecoolcheck = true;
        }

    }

    public void GetDamage(int damage)
    {
        hpNow -= damage;

        if (hpNow <= 0)
        {
            animator.SetTrigger("Death");
            Death();
        }
        else
            animator.SetTrigger("Hit");
    }

    public void Death()
    {

        Instantiate(Coin, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, Actor.transform);
    }

    public void Animation()
    {
        animator.SetBool("Move", isMove);
    }

    public void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, col.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f);

        if(hit.collider !=null&&hit.collider.tag ==("NPC")|| hit.collider != null && hit.collider.tag == ("Projectile"))
        {
            return;
        }
        bool grounded = hit.collider != null && hit.collider.CompareTag("Ground");
        isGround = grounded;

        if (!isGround)
            patrollTimer = -patrollTimer;
    }

    IEnumerator IFollow()
    {
        float timer = returnTimer; //timer to return after palyer leave

        while (isFollowing)
        {
            if (Vector2.Distance(transform.position, followTarget.position) > returnRadius && timer <= 0) //if target leave
            {
                isFollowing = false;
                followTarget = null;

                isReturning = true;

                patrollSin = 0;

                StartCoroutine(IReturnToStartPos()); //start returning
            }
            else if (!isGround) //if the target jumps over the gap  
            {
                isFollowing = false;
                followTarget = null;

                isReturning = true;

                patrollSin = 0;

                StartCoroutine(IReturnToStartPos());
            }
            else
            {
                if (Vector2.Distance(transform.position, followTarget.position) > rangeRadius && !isAttack && !isRangeAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName("BrokenClockwoker_Melee_Right_Animation")&& !animator.GetCurrentAnimatorStateInfo(0).IsName("BrokenClockwoker_Range_Right_Ani"))
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
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BrokenClockwoker_Melee_Right_Animation") && !animator.GetCurrentAnimatorStateInfo(0).IsName("BrokenClockwoker_Range_Right_Ani") && RangeSkillCoolTimeNow != 0f && Vector2.Distance(transform.position, followTarget.position) < rangeRadius && Vector2.Distance(transform.position, followTarget.position) > attackRadius)
                {
                    isMove = true;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(followTarget.position.x, transform.position.y), speed * Time.deltaTime);
                }


                timer -= Time.deltaTime;
            }
            yield return null;
        }

        yield return null;
    }

    IEnumerator IReturnToStartPos()
    {
        isMove = true;
        while (transform.position.x != starterPos.x)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(starterPos.x, transform.position.y), speed * Time.deltaTime); //move to start pos

            if (hpNow < hpMax)
            {
                hpNow = hpMax;
            }

            yield return null;
        }

        isReturning = false;
        yield return null;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.DrawWireSphere(transform.position, returnRadius);
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}
