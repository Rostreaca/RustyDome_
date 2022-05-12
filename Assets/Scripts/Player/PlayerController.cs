using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public int hpMax;
    public int hpNow;
    public int powerMax;
    public int powerNow;
    public int powerRegen;
    public int ammoMax;
    public int ammoNow;
    public int def;
    public int money;

    public MeleeWeapon meleeWeapon;
    public RangeWeapon rangeWeapon;

    public int moveSpeed;
    //대시쿨타임
    public float dashCoolTime;
    //대시하는시간
    public float dashTime;
    //대시용타이머
    public float dashTimer;
    public float jumpPower;
    public float dashSpeed;

    private Rigidbody2D rigid;
    private CapsuleCollider2D col;
    private Animator animator;
    private DashState dashState = DashState.Ready;
    private bool isDash = false;
    private bool isGround = false;
    public bool onLadder = false;
    [SerializeField]
    private bool isClimb = false;
    public bool isAttack = false;

    private enum DashState
    {
        Ready,
        Dashing,
        Cooldown,
    }
    void SIngleton_Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    void Awake()
    {
        SIngleton_Init();
    }
    // Start is called before the first frame update

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void FixedUpdate()
    {
        if (GameManager.Instance.isGame && !GameManager.Instance.isPause) //check Game status
        {
            Move();
            Climb();
            GroundCheck();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGame && !GameManager.Instance.isPause)
        {
            Rotation();
            Dash();
            Attack();
            Jump();
            Animation();
        }
    }

    private void Move()
    {
        if(!isDash && !isClimb && !isAttack)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        }
    }

    private void Rotation()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Dash()
    {
        switch (dashState)
        {
            case DashState.Ready:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (Mathf.Round(Input.GetAxis("Horizontal")) != 0)
                    {
                        animator.SetTrigger("Dash");
                        rigid.velocity = Vector2.right * Input.GetAxis("Horizontal") * dashSpeed;
                        rigid.gravityScale = 0;
                        dashTimer = dashCoolTime;
                        dashState = DashState.Dashing;
                        isDash = true;
                    }
                }

                break;

            case DashState.Dashing:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= dashCoolTime - dashTime)
                {
                    rigid.velocity = Vector2.zero;
                    rigid.gravityScale = 1;
                    dashState = DashState.Cooldown;
                    isDash = false;
                }
                break;

            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer < 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround && !isAttack)
            {
                //S를 누르고 있을경우엔 하단점프(점프안함)
                if (!Input.GetKey(KeyCode.S))
                {
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                }

                animator.SetTrigger("Jump");
                isClimb = false;
            }
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, col.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f);
        bool grounded = hit.collider != null && hit.collider.CompareTag("Ground");
        grounded = grounded || isClimb;
        if(hit.collider!=null && hit.collider.CompareTag("NPC"))
        {
            isGround = true;
            return;
        }
        isGround = grounded;
    }

    private void Climb()
    {
        if (onLadder)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                isClimb = true;
                rigid.velocity = Vector2.up * Input.GetAxis("Vertical") * moveSpeed;
            }
            else
            {
                rigid.velocity = Vector2.zero;
            }
        }

        else
        {
            isClimb = false;
        }

        if (isClimb)
            rigid.gravityScale = 0;
        else
            rigid.gravityScale = 1;
    }

    private void Attack()
    {
        if (!isAttack && !isClimb)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                isAttack = true;

                if (Input.GetMouseButton(0))
                {
                    animator.SetBool(meleeWeapon.animName, isAttack);
                }

                if (Input.GetMouseButton(1))
                {
                    animator.SetBool("RangeAttack", isAttack);
                }
            }
        }
    }

    public void Animation()
    {
        if (!isClimb) //Lader check
            if (Input.GetAxis("Horizontal") != 0)
                animator.SetBool("Move", true);
            else
                animator.SetBool("Move", false);
        else
        {
            animator.SetBool("Move", false);
        }

        animator.SetBool("isGround", isGround);
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
        GameManager.Instance.GameOver(); //set game state to game over
    }

}
