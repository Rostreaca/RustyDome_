using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyManager
{
    public float patrollRadius;
    public float patrollSin;
    public float returnRadius;
    public float attackRadius;
    public Transform followTarget;
    public GameObject Coin;
    public GameObject Actor;

    private CapsuleCollider2D col;
    private SpriteRenderer rend;
    private Animator animator;

    private Vector2 starterPos;
    private float patrollTimer = 0.02f;
    private float returnTimer = 3;
    private bool canMove;
    private bool isMove;
    private bool isAttack;
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

            Rotation();
            Animation();
        }
    }

    public void Move()
    {
        GroundCheck();

        if (!isAttack)
            isMove = true;

        if (isPatrolling && !isFollowing && !isReturning)
        {
            Patroll();
        }
    }

    private void Patroll()
    {
        patrollSin += patrollTimer * Speed;

        if(patrollSin >= 1)
        {
            patrollTimer = -0.02f;
        }
        else if (patrollSin <= -1)
        {
            patrollTimer = 0.02f;
        }

        float x = patrollRadius * patrollSin + starterPos.x;

        transform.position = Vector2.MoveTowards(transform.position,  new Vector2(x, transform.position.y), Speed * Time.deltaTime);
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
        isAttack = true;

        animator.SetTrigger("MeleeAttack");
    }

    public void GetDamage(int damage)
    {
        hpNow -= damage;

        if (hpNow <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animator.SetTrigger("Death");

        Destroy(gameObject);
        Instantiate(Coin, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, Actor.transform);
    }

    public void Animation()
    {
        animator.SetBool("Move", isMove);
    }

    public void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, col.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f);

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
                if (Vector2.Distance(transform.position, followTarget.position) > attackRadius && !isAttack)
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(followTarget.position.x, transform.position.y), Speed / 2 * Time.deltaTime); //follow target
                else //if target in attack radius
                    Attack(); //attack

                timer -= Time.deltaTime;
            }
            yield return null;
        }

        yield return null;
    }

    IEnumerator IReturnToStartPos()
    {
        while (transform.position.x != starterPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(starterPos.x, transform.position.y), Speed / 2 * Time.deltaTime); //move to start pos

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
    }
}
