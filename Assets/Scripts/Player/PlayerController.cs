using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform PlayerPos;
    public static PlayerController instance;

    public float hpMax;
    public float hpNow;
    public float playerHp;
    public float moduleHp;

    public float powerMax;
    public float powerNow;
    public float playerPower;
    public float modulePower;

    public int powerRegen;
    public int playerPowerRegen;
    public int modulePowerRegen;

    public int ammoMax;
    public int ammoNow;
    public int scrap;

    public float moveSpeed;
    public float playerMoveSpeed;
    public float moduleMoveSpeed;

    public float attackSpeed;
    public float playerAttackSpeed;
    public float moduleAttackSpeed;

    public List<CustomizeSlot> moduleEquipSlots = new List<CustomizeSlot>();

    public PlayerCombat combat;
    public MeleeWeapon meleeWeapon;
    public RangeWeapon rangeWeapon;
    public SpecialWeapon specialWeapon;

    public int meleeDmg;
    public float moduleDmg;

    public float rollCoolTime = 0.5f;
    public float rollTimer;
    public float rollTime = 0.5f;
    public float rollSpeed;
    public float jumpPower;
    public float noHitTime = 0.5f;
    public float forcePower;
    public float upperForcePower;
    public float gravityScale = 1f;

    public bool isGround = false;
    public bool onLadder = false;
    public bool isClimb = false;
    public bool isRoll = false;
    public bool isMeleeAttack = false;
    public bool isAiming = false;
    public bool isRangeAttack = false;
    public bool isSpecialAttack = false;
    public bool istalking = false;
    public bool isOpeningBox;

    public bool isDeath = false;

    public bool isAttack
    {
        get
        {
            return isMeleeAttack || isRangeAttack || isSpecialAttack || isAiming;
        }
    }
    public bool isHit = false;
    public bool isInvincible = false;
    public bool isCharge = false;

    public Animator animator;
    public SpriteRenderer spriteRenderer, rangeEffectSpriteRenderer;
    public PlayerWeaponSprite weaponSprite;

    private Rigidbody2D rigid;
    public CapsuleCollider2D col;
    
    [SerializeField]
    private bool canAirJump;

    private void SIngleton_Init()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Awake()
    {
        SIngleton_Init();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        combat = GetComponentInChildren<PlayerCombat>();

        for (int i=0; i<5; i++)
        {
            moduleEquipSlots.Add(GameObject.Find("ModuleEquipSlot" + i).GetComponent<CustomizeSlot>());
        }

        StateUpdate();
    }

    public void FixedUpdate()
    {
        //!animator.GetCurrentAnimatorStateInfo(0).IsName("char_analyze_start")&&!animator.GetCurrentAnimatorStateInfo(0).IsName("char_analyze_end")
        PlayerPos = transform;
        if (GameManager.Instance.BossCutscenePlaying == false && GameManager.Instance.RuinCutscenePlaying == false && GameManager.Instance.NowLoading == false && !istalking&&!isOpeningBox)
        {
            if (GameManager.Instance.isGame && !GameManager.Instance.isPause&& !animator.GetCurrentAnimatorStateInfo(0).IsName("char_analyze_start") && !animator.GetCurrentAnimatorStateInfo(0).IsName("char_analyze_end")) //check Game status
            {
                Move();
                Climb();
                GroundCheck();
            }
        }

        else
        {
            animator.SetBool("Move",false);
            animator.SetBool("isGround", true);
            animator.SetBool("Climb", false) ;
        }
            animator.SetBool("isTalking", istalking);
    }

    private void Update()
    {
        if(GameManager.Instance.BossCutscenePlaying == false && GameManager.Instance.RuinCutscenePlaying == false && GameManager.Instance.NowLoading == false && !istalking&&!isOpeningBox)
        {
            if (GameManager.Instance.isGame && !GameManager.Instance.isPause&& !animator.GetCurrentAnimatorStateInfo(0).IsName("char_analyze_start") && !animator.GetCurrentAnimatorStateInfo(0).IsName("char_analyze_end"))
            {
                Rotate();
                Roll();
                Attack();
                Jump();
                Animation();

                PowerRegen();
            }
        }
    }

    private void Move()
    {
        if (!isRoll && !isClimb && !isAttack && !isHit &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        }
    }

    private void Rotate()
    {
        if (!isRoll && !isClimb && !isAttack && !isHit)
        {
            //대략 0.015f = 1px
            float rangeEffectSpriteOffset = 0.5f;

            //왼쪽
            if (Input.GetAxis("Horizontal") < 0)
            {
                spriteRenderer.flipX = true;

                rangeEffectSpriteRenderer.flipX = true;
                rangeEffectSpriteRenderer.gameObject.transform.localPosition = new Vector3(-rangeEffectSpriteOffset, -0.12f, 0);
            }

            //오른쪽
            else if (Input.GetAxis("Horizontal") > 0)
            {
                spriteRenderer.flipX = false;
                
                rangeEffectSpriteRenderer.flipX = false;
                rangeEffectSpriteRenderer.gameObject.transform.localPosition = new Vector3(rangeEffectSpriteOffset, -0.12f, 0);
            }

            weaponSprite.Rotate();
        }
    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGround && powerNow > 30 &&
            !isMeleeAttack && !isRangeAttack && !isSpecialAttack && !isHit)
        {
            int dir = Mathf.CeilToInt(Input.GetAxis("Horizontal"));
            if (rollTimer == 0 && dir != 0)
            {
                //원거리공격취소
                isAiming = false;
                animator.SetBool(rangeWeapon.animName, false);

                animator.SetTrigger("Roll");

                isClimb = false;
                isRoll = true;

                rigid.gravityScale = 0;
                powerNow -= 50;
                StartCoroutine(IRoll(dir));
            }
        }
    }

    private IEnumerator IRoll(int dir)
    {
        Vector3 targetVec = transform.position + Vector3.right * dir * rollSpeed * rollTime;

        while (rollTimer < rollCoolTime)
        {
            if (isRoll)
            {
                if (rollTimer < rollTime) //dashing
                {
                    
                    transform.position = Vector3.MoveTowards(transform.position, targetVec, rollSpeed * Time.deltaTime);
                }

                else
                {
                    rigid.velocity = Vector2.zero;
                    rigid.gravityScale = gravityScale;
                    isRoll = false;
                }
            }

            rollTimer += Time.deltaTime;
            yield return null;
        }

        rollTimer = 0;
        yield return null;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && powerNow >= 20 &&
            !isHit && !isAttack && !isRoll)
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
                powerNow -= 30;
            }

            //else if (canAirJump)
            //{
            //    rigid.velocity = new Vector2(rigid.velocity.x, 0);
            //    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            //    canAirJump = false;
            
            //    animator.SetTrigger("Jump");
            //    powerNow -= 20;
            //}
        }
    }

    private void GroundCheck()
    {
        Vector2 start = col.bounds.center + (Vector3.down * (col.bounds.size.y / 2));
        LayerMask layerMask = (1 << LayerMask.NameToLayer("Platform")) + (1 << LayerMask.NameToLayer("Ground"));
        RaycastHit2D hit = Physics2D.BoxCast(start, new Vector2(col.bounds.size.x * 0.75f, 0.1f), 0, Vector2.down, 0.1f, layerMask);
        bool grounded = hit.collider != null;

        grounded = grounded || isClimb;
        if(hit.collider != null && hit.collider.CompareTag("NPC"))
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
        if (onLadder && !isAttack && !isRoll && !isHit)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isClimb = true;
                animator.SetBool("Climb", true);

                transform.position = new Vector2(Mathf.Floor(transform.position.x) + 0.5f, transform.position.y);
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
                RaycastHit2D hit = Physics2D.CapsuleCast(col.bounds.center, col.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, layerMask);

                if (hit)
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        isClimb = false;
                        animator.SetBool("Climb", false);
                    }

                    else
                    {
                        isClimb = true;
                        animator.SetBool("Climb", true);

                        transform.position = new Vector2(Mathf.Floor(transform.position.x) + 0.5f, transform.position.y);
                    }
                }

                else
                {
                    isClimb = true;
                    animator.SetBool("Climb", true);

                    transform.position = new Vector2(Mathf.Floor(transform.position.x) + 0.5f, transform.position.y);
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
            rigid.gravityScale = gravityScale;
        }
    }

    private void Attack()
    {
        if (!isAttack && !isClimb && !isHit && !isRoll)
        {
            //Z버튼 근접공격
            if ((Input.GetKeyDown(KeyCode.Z)) && (meleeWeapon != null))
            {
                if (powerNow >= meleeWeapon.powerCon)
                {
                    isMeleeAttack = true;

                    powerNow -= meleeWeapon.powerCon;
                    animator.SetBool(meleeWeapon.animName, true);
                }
            }

            //X버튼 원거리공격
            if (Input.GetKeyDown(KeyCode.X))
            {
                isAiming = true;
                animator.SetBool(rangeWeapon.animName, true);
            }

            //휠클릭, C버튼 특수공격
            if (Input.GetKeyDown(KeyCode.C) && specialWeapon != null)
            {
                if (powerNow >= specialWeapon.powerCon)
                {
                    isSpecialAttack = true;

                    powerNow -= specialWeapon.powerCon;
                    animator.SetBool(specialWeapon.animName, true);
                }
            }
        }
    }

    public void Animation()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("Move", true);
        }

        else
        {
            animator.SetBool("Move", false);
        }

        if (isClimb)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                animator.SetFloat("ClimbSpeed", 1);
            }

            else
            {
                animator.SetFloat("ClimbSpeed", 0);
            }
        }

        animator.SetBool("isGround", isGround);
    }

    public void GetDamage(int damage, Transform enemy)
    {
        if (isInvincible == false)
        {
            animator.SetTrigger("Hit");
            weaponSprite.Hit();
            hpNow -= damage;

            if (meleeWeapon != null)
            {
                isMeleeAttack = false;
                animator.SetBool(meleeWeapon.animName, false);
            }

            if (rangeWeapon != null)
            {
                isAiming = false;
                isRangeAttack = false;
                animator.SetBool(rangeWeapon.animName, false);
            }

            if (specialWeapon != null)
            {
                isSpecialAttack = false;
                animator.SetBool(specialWeapon.animName, false);
            }


            combat.Hit();

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
        isInvincible = true;

        yield return new WaitForSeconds(noHitTime);

        if (!isDeath)
        {
            isHit = false;
            isInvincible = false;
        }
    }

    public void Death()
    {
        isDeath = true;
        animator.SetTrigger("Death");
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
        hpNow = hpMax;
        powerMax = playerPower + modulePower;
        powerRegen = playerPowerRegen + modulePowerRegen;
        moveSpeed = playerMoveSpeed * (1 + moduleMoveSpeed);
        attackSpeed = playerAttackSpeed * (1 + moduleAttackSpeed);

        meleeDmg = (int)Mathf.Round(meleeWeapon.dmg * (1 + moduleDmg));

        animator.SetFloat("MoveSpeed", 1 + moduleMoveSpeed);
        animator.SetFloat("AttackSpeed", 1 + moduleAttackSpeed);
        weaponSprite.gameObject.GetComponent<Animator>().SetFloat("AttackSpeed", 1 + moduleAttackSpeed);

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
        moduleMoveSpeed = 0;
        moduleAttackSpeed = 0;
        moduleDmg = 0;

        foreach (CustomizeSlot slot in moduleEquipSlots)
        {
            if (slot.item != null)
            {
                Module module = slot.item as Module;

                moduleHp += module.hp;
                modulePower += module.power;
                modulePowerRegen += module.powerRegen;
                moduleMoveSpeed += module.moveSpeed;
                moduleAttackSpeed += module.attackSpeed;
                moduleDmg += module.meleeDmg;
            }
        }
    }

    public void GetScrap(int value)
    {
        scrap += value;
    }

}
