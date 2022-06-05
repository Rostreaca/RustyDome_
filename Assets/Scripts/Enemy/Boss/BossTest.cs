using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    public Animator anim;

    public static BossTest instance;
    
    [Header("Pattern1 Parameters")]
    public Transform HandPos;
    public Transform TargetPos;
    public Transform handmovecontrol;
    public bool isrollback;
    public bool isattack;
    public float p1cooltimer;
    public bool ismemory = true;
    public bool p1coolstart;
    public Vector2 saveTargetPos;
    private float init_p1cool;
    [Header("Pattern2 Parameters")]
    public Rigidbody2D rigid;
    public bool isRush;
    public bool isattack2;
    public bool leftmove, rightmove;
    public float p2cooltimer;
    private float init_p2cool;
    private Vector2 saveBossVelocity;
    void SingletonInit()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Awake()
    {
        TargetPos = GameObject.Find("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SingletonInit();
        saveBossVelocity = rigid.velocity;
        init_p1cool = p1cooltimer;
        init_p2cool = p2cooltimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(BossController.instance.isdead != true) // 보스가 살아있을 경우에만 작동
        {
            
            rollbackPos();
            memory();
            followcondition();
            if (isattack == true)
            {
                Attack1();//타겟포지션을 저장해놓고 이동하게 해서 계속 추적하지 않게 함.
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossPattern2AtkPrePare") == true && isattack2 == true)
            {
                Attack2();
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossIdle") == true) //애니메이터에서 boss_idle이 실행될때만 플립하게 설정.
            {
                flip();
            }
            pattern1cooldown();
            pattern2cooldown();
        }
        if(BossController.instance.isdead == true)
        {
            rigid.velocity = saveBossVelocity;
        }
    }
    public void Attack1()
    {
        if (HandPos.position != TargetPos.position)
            HandPos.position = Vector2.MoveTowards(HandPos.position, saveTargetPos, 1f * Time.deltaTime);
                
    }

    public void rollbackPos()
    {
        if(isrollback == true)
        {
            HandPos.position = Vector2.MoveTowards(HandPos.position, handmovecontrol.position, 10f * Time.deltaTime);
        }
    }
    public void memory()
    {
        if(ismemory == true)
        {
            saveTargetPos.x = TargetPos.position.x;
        }
    }

    public void flip()
    {
        if(transform.position.x <= TargetPos.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

    public void Attack2() //isattack2가 트루면 실행
    {
        if (isRush == true) // isRush가 트루면 실행
        {
            if (transform.position.x > saveTargetPos.x && rightmove == false)// 왼쪽에 플레이어가 있으면 추격.
            {
                rigid.velocity = new Vector2(-8, 0);
                leftmove = true;
            }
            else if(transform.position.x < saveTargetPos.x&&leftmove == false)
            {
                rigid.velocity = new Vector2(8, 0);
                rightmove = true;
            }
            else if(transform.position.x == TargetPos.position.x)//위치가 같을 경우 2번패턴의 방향
            {
                if(transform.localScale == new Vector3(1, 1, 1))//정방향(왼쪽)을 바라보고 있을때
                {
                    rigid.velocity = new Vector2(-8, 0);
                    leftmove = true;
                }
                else if(transform.localScale == new Vector3(-1, 1, 1))//역방향(오른쪽)을 바라보고 있을때
                {
                    rigid.velocity = new Vector2(8, 0);
                    rightmove = true;
                }
            }
        }
        if(anim.GetBool("Pattern2start") == false)
        {
            isattack2 = false;
            isRush = false;
            leftmove = false;
            rightmove = false;
            rigid.velocity = saveBossVelocity;
        }
    }//attack2가 한번 실행되면 벽에 박을 때 까지 방향을 꺾어선 안됨.

    public void pattern1cooldown()
    {
        if (p1coolstart == true)
        {
            
            anim.SetBool("Pattern1isCool", true);
        }
        if (p1cooltimer > 0 && anim.GetBool("Pattern1isCool")==true)
        {
            p1cooltimer -= Time.deltaTime;
        }

        if (p1cooltimer <= 0) 
        {
            anim.SetBool("Pattern1isCool", false);
            if (anim.GetBool("Pattern1start") == true)//쿨타임을 저장해 놓은 값으로 초기화.
            {
                p1cooltimer = init_p1cool;
            }
        }
    }
    public void pattern2cooldown()
    {
        if(p2cooltimer <= 0) //쿨이 돌았는데 벽에 안박혀있을 경우 그냥 패턴2 쿨이 돌아가게 실행. & 쿨이 돌았는데 벽에 박혀있을 경우 쿨은 돌지만 패턴2가 실행되지 않음.
        {
            anim.SetBool("Pattern2isCool", false);
            if(anim.GetBool("Pattern2start") == true)//쿨타임을 저장해 놓은 값으로 초기화.
            {
                p2cooltimer = init_p2cool;
            }
        }
        if(p2cooltimer > 0 && anim.GetBool("Pattern2isCool")==true)
        {
            p2cooltimer -= Time.deltaTime;
        }
    }

    public void follow()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossIdle") == true)
        {
            if (transform.localScale == new Vector3(-1, 1, 1))//역방향(오른쪽)을 바라보고 있을때
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(TargetPos.position.x - 2.5f, transform.position.y), 1.2f * Time.deltaTime);
            }
            else if(transform.localScale == new Vector3(1, 1, 1))//정방향(왼쪽)을 바라보고 있을때
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(TargetPos.position.x + 2.5f, transform.position.y), 1.2f * Time.deltaTime);
            }
        }
    }
    public void followcondition()
    {

        if (anim.GetBool("Pattern1isCool") == true && anim.GetBool("Pattern2isCool") == true || anim.GetBool("Pattern1isCool") == true && anim.GetBool("Pattern1start") == true || anim.GetBool("Pattern2isCool") == true && anim.GetBool("Pattern2start") == true )//스킬 두개가 모두 쿨타임인 경우 && 스킬 1이 쿨타임인데 스킬1 범위에 들어온 경우 && 스킬 2가 쿨타임인데 스킬2 범위에 들어온 경우
        {
            follow();
        }
    }
    public void SoundPlay(AudioClip audio)
    {
        SoundManager.instance.SFXPlay("aa",audio);
    }
}
