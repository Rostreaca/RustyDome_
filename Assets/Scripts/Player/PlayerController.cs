using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float hpMax;
    public float hpNow;
    public float powerMax;
    public float powerNow;
    public int powerRegen;
    public int ammoMax;
    public int ammoNow;
    public int def;
    public int money;

    public MeleeWeapon meleeWeapon;
    public RangeWeapon rangeWeapon;

    public float moveSpeed;
    public float dashCoolTime;
    public float dashTimer;
    public float dashTime;
    public float dashSpeed;
    public float jumpPower;
    public float noHitTime=0.5f;
    public float forcePower;
    public float upperForcePower;

    public bool onLadder = false;
    public bool isClimb = false;
    public bool isAttack = false;
    public bool ishit = false;

    public Animator animator;

    private Rigidbody2D rigid;
    private CapsuleCollider2D col;
    private bool isDash = false;
    private bool isGround = false;
    [SerializeField]
    private bool canAirJump;


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

            PowerRegen();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Inventory.instance.AddItem(Inventory.instance.testItem);
        }
    }

    private void Move()
    {
        if(!isDash && !isClimb && !isAttack && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit_Left_Ani")&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit_Right_Ani"))
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        }
    }

    private void Rotation()
    {
        if (!isDash && !isAttack)
        {
            if (Input.GetAxis("Horizontal") > 0)
                animator.SetBool("Flip", false);
            else
                animator.SetBool("Flip", true);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && powerNow >30 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit_Right_Ani") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit_Left_Ani"))
        {
            int dir = Mathf.CeilToInt(Input.GetAxis("Horizontal"));
            if (dashTimer == 0 && dir != 0)
            {
                animator.SetTrigger("Dash");
                rigid.gravityScale = 0;
                isDash = true;
                StartCoroutine(IDash(dir));
                powerNow -= 30;
            }
        }
    }

    IEnumerator IDash(int dir)
    {
        while (dashTimer < dashCoolTime)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit_Right_Ani")&& animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit_Left_Ani"))
            {
                dashTimer = dashCoolTime;
            }
            if (isDash)
            {
                if (dashTimer < dashTime) //dashing
                {
                    rigid.velocity = new Vector2(dir * dashSpeed, 0);
                }

                else
                {
                    rigid.velocity = Vector2.zero;
                    rigid.gravityScale = 1;
                    isDash = false;
                }
            }

            dashTimer += Time.deltaTime;
            yield return null;
        }

        dashTimer = 0;
        yield return null;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && powerNow > 20)
        {
            if (!isAttack)
            {
                if (isGround)
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, 0);
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                    if (isClimb)
                    {
                        isClimb = false;
                        animator.SetBool("Climb", false);
                    }

                    animator.SetTrigger("Jump");
                    powerNow -= 20;
                }
                
                else if (canAirJump)
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, 0);
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    canAirJump = false;

                    if (isClimb)
                    {
                        isClimb = false;
                        animator.SetBool("Climb", false);
                    }

                    animator.SetTrigger("Jump");
                    powerNow -= 20;
                }
            }
        }
    }

    private void GroundCheck()
    {
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, col.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, layerMask);
        bool grounded = hit.collider != null;

        grounded = grounded || isClimb;
        if(hit.collider!=null && hit.collider.CompareTag("NPC"))
        {
            isGround = true;
            return;
        }

        if (grounded)
            canAirJump = true;

        isGround = grounded;
    }

    private void Climb()
    {
        if (onLadder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                isClimb = true;
                animator.SetBool("Climb", true);
            }

            else if (Input.GetKey(KeyCode.S))
            {
                LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
                RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, col.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, layerMask);

                if (hit.collider != null)
                {
                    isClimb = false;
                    animator.SetBool("Climb", false);
                }

                else
                {
                    isClimb = true;
                    animator.SetBool("Climb", true);
                }
            }
        }

        if (isClimb)
        {
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.up * Input.GetAxis("Vertical") * moveSpeed;
        }

        else 
        {
            rigid.gravityScale = 1;
        }
    }

    private void Attack()
    {
        if (!isAttack && !isClimb)
        { 
            if(powerNow > 15)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                    isAttack = true;

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (powerNow > meleeWeapon.powerCon)
                        {
                            powerNow -= meleeWeapon.powerCon;
                            animator.SetBool(meleeWeapon.animName, isAttack);
                            powerNow -= 15;
                        }
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        if (powerNow > rangeWeapon.powerCon)
                        {
                            powerNow -= rangeWeapon.powerCon;
                            animator.SetBool("RangeAttack", isAttack);
                            powerNow -= 15;
                        }

                    }
                }
             
            }
        }
    }

    public void Animation()
    {
        //Lader check
        if (!isClimb)
        {
            if (Input.GetAxis("Horizontal") != 0)
                animator.SetBool("Move", true);

            else
                animator.SetBool("Move", false);
        }

        else
        {
            animator.SetBool("Move", false);
        }

        animator.SetBool("isGround", isGround);
    }

    public void GetDamage(int damage, Transform enemy)
    {
        if(ishit == false)
        {
            animator.SetTrigger("Hit");
            hpNow -= damage;

            Vector2 dir = enemy.position.x < transform.position.x ? Vector2.right : Vector2.left;
            rigid.AddForce(dir * forcePower + Vector2.up * upperForcePower);
            StartCoroutine(IHit());

            if (hpNow <= 0)
            {
                Death();
            }
        }

    }

    private IEnumerator IHit()
    {
        ishit = true;

        yield return new WaitForSeconds(noHitTime);

        ishit = false;
    }

    public void Death()
    {
        animator.SetTrigger("Death");
        GameManager.Instance.GameOver(); //set game state to game over
    }

    public void PowerRegen()
    {
        if (powerNow != powerMax)
        {
            powerNow += powerRegen * Time.deltaTime;
        }
        if (powerNow > powerMax)
        {
            powerNow = powerMax;
        }
    }

}
