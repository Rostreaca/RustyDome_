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
    public int scrap;

    public float playerHp;
    public float playerPower;
    public int playerPowerRegen;

    public float moduleHp;
    public float modulePower;
    public int modulePowerRegen;

    public List<CustomizeSlot> moduleEquipSlots = new List<CustomizeSlot>();

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

    public bool isGround = false;
    public bool onLadder = false;
    public bool isClimb = false;
    public bool isAttack = false;
    public bool isHit = false;
    public bool isCharge = false;

    public Animator animator;

    private Rigidbody2D rigid;
    private CapsuleCollider2D col;
    private bool isDash = false;
    [SerializeField]
    private bool canAirJump;

    private void SIngleton_Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Awake()
    {
        SIngleton_Init();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit_Left_Ani"))
            animator.SetBool("Flip", true);
        else
            animator.SetBool("Flip", true);

        for (int i=0; i<5; i++)
        {
            moduleEquipSlots.Add(GameObject.Find("ModuleEquipSlot" + i).GetComponent<CustomizeSlot>());
        }

        StateUpdate();
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

    private void Update()
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
    }

    private void Move()
    {
        if(!isDash && !isClimb && !isAttack && !isHit)
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
            else if(Input.GetAxis("Horizontal") < 0)
                animator.SetBool("Flip", true);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isAttack && !isHit && isGround && powerNow >  30)
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

    private IEnumerator IDash(int dir)
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
        if (Input.GetKeyDown(KeyCode.Space) && !isHit && powerNow > 20)
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

                    powerNow -= 20;
                }
            }
        }
    }

    private void GroundCheck()
    {
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, new Vector2(col.bounds.size.x * 0.75f, col.bounds.size.y), CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, layerMask);
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
        if (!isAttack && !isClimb && !isHit)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (powerNow >= meleeWeapon.powerCon)
                {
                    isAttack = true;

                    powerNow -= meleeWeapon.powerCon;
                    animator.SetBool(meleeWeapon.animName, true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (powerNow >= rangeWeapon.powerCon && ammoNow >= rangeWeapon.ammoCon)
                {
                    isAttack = true;

                    powerNow -= rangeWeapon.powerCon;
                    ammoNow -= rangeWeapon.ammoCon;
                    animator.SetBool(rangeWeapon.animName, true);
                }

            }

            if (isCharge)
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    isAttack = true;

                    animator.SetTrigger(meleeWeapon.skillAnimName);
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
        if (isHit == false)
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
        isHit = true;

        yield return new WaitForSeconds(noHitTime);

        isHit = false;
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

    public void StateUpdate()
    {
        ModuleUpdate();

        hpMax = playerHp + moduleHp;
        powerMax = playerPower + modulePower;
        powerRegen = playerPowerRegen + modulePowerRegen;

        if (hpNow > hpMax)
            hpNow = hpMax;

        if (powerNow > powerMax)
            powerNow = powerMax;
    }

    public void ModuleUpdate()
    {
        moduleHp = 0;
        modulePower = 0;
        modulePowerRegen = 0;

        foreach (CustomizeSlot slot in moduleEquipSlots)
        {
            if (slot.item != null)
            {
                Module module = slot.item as Module;

                moduleHp += module.hp;
                modulePower += module.power;
                modulePowerRegen += module.powerRegen;
            }
        }
    }



    public void GetScrap(int value)
    {
        scrap += value;
    }

}
