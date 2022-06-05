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
        if(BossController.instance.isdead != true) // ������ ������� ��쿡�� �۵�
        {
            
            rollbackPos();
            memory();
            followcondition();
            if (isattack == true)
            {
                Attack1();//Ÿ���������� �����س��� �̵��ϰ� �ؼ� ��� �������� �ʰ� ��.
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossPattern2AtkPrePare") == true && isattack2 == true)
            {
                Attack2();
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossIdle") == true) //�ִϸ����Ϳ��� boss_idle�� ����ɶ��� �ø��ϰ� ����.
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

    public void Attack2() //isattack2�� Ʈ��� ����
    {
        if (isRush == true) // isRush�� Ʈ��� ����
        {
            if (transform.position.x > saveTargetPos.x && rightmove == false)// ���ʿ� �÷��̾ ������ �߰�.
            {
                rigid.velocity = new Vector2(-8, 0);
                leftmove = true;
            }
            else if(transform.position.x < saveTargetPos.x&&leftmove == false)
            {
                rigid.velocity = new Vector2(8, 0);
                rightmove = true;
            }
            else if(transform.position.x == TargetPos.position.x)//��ġ�� ���� ��� 2�������� ����
            {
                if(transform.localScale == new Vector3(1, 1, 1))//������(����)�� �ٶ󺸰� ������
                {
                    rigid.velocity = new Vector2(-8, 0);
                    leftmove = true;
                }
                else if(transform.localScale == new Vector3(-1, 1, 1))//������(������)�� �ٶ󺸰� ������
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
    }//attack2�� �ѹ� ����Ǹ� ���� ���� �� ���� ������ ��� �ȵ�.

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
            if (anim.GetBool("Pattern1start") == true)//��Ÿ���� ������ ���� ������ �ʱ�ȭ.
            {
                p1cooltimer = init_p1cool;
            }
        }
    }
    public void pattern2cooldown()
    {
        if(p2cooltimer <= 0) //���� ���Ҵµ� ���� �ȹ������� ��� �׳� ����2 ���� ���ư��� ����. & ���� ���Ҵµ� ���� �������� ��� ���� ������ ����2�� ������� ����.
        {
            anim.SetBool("Pattern2isCool", false);
            if(anim.GetBool("Pattern2start") == true)//��Ÿ���� ������ ���� ������ �ʱ�ȭ.
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
            if (transform.localScale == new Vector3(-1, 1, 1))//������(������)�� �ٶ󺸰� ������
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(TargetPos.position.x - 2.5f, transform.position.y), 1.2f * Time.deltaTime);
            }
            else if(transform.localScale == new Vector3(1, 1, 1))//������(����)�� �ٶ󺸰� ������
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(TargetPos.position.x + 2.5f, transform.position.y), 1.2f * Time.deltaTime);
            }
        }
    }
    public void followcondition()
    {

        if (anim.GetBool("Pattern1isCool") == true && anim.GetBool("Pattern2isCool") == true || anim.GetBool("Pattern1isCool") == true && anim.GetBool("Pattern1start") == true || anim.GetBool("Pattern2isCool") == true && anim.GetBool("Pattern2start") == true )//��ų �ΰ��� ��� ��Ÿ���� ��� && ��ų 1�� ��Ÿ���ε� ��ų1 ������ ���� ��� && ��ų 2�� ��Ÿ���ε� ��ų2 ������ ���� ���
        {
            follow();
        }
    }
    public void SoundPlay(AudioClip audio)
    {
        SoundManager.instance.SFXPlay("aa",audio);
    }
}
