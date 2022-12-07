using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NPCController : NPCManager
{
    public static NPCController instance;

    public Animator anim;

    public GameObject Item;
    private void Singleton_Init()
    {
         instance = this;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Singleton_Init();
    }
    public void Start()
    {
        playerPos = GameObject.Find("Player").transform;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //if (SceneManage.Instance.nowscene.buildIndex == 4 && anim.GetCurrentAnimatorStateInfo(0).IsName("Box1_Open"))
        //{
        //    Check();
        //    findDialog();
        //}
        int a = SceneManager.GetActiveScene().buildIndex;
        if(name == "lever_nohandle" && GameManager.Instance.isDoorOpen)
        {
            anim.SetBool("HandleWork",true);
            return;
        }
        if(tag == "Chest" )
        {
            if (name == "Chest_2")
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Box1_Open"))
                {
                    GameManager.Instance.boxopened[a+1] = true;
                }
                if (GameManager.Instance.boxopened[a+1] == true)
                {
                    anim.SetTrigger("Open");
                }
                else
                {
                    Check();
                    CreateTextBox();
                    findDialog();
                }
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Box1_Open"))
                {
                    if (SceneManage.Instance.nowscene.buildIndex == 4)
                    {
                        CreateTextBox();
                        findDialog();
                        Check();
                    }
                    GameManager.Instance.boxopened[a] = true;
                }
                if (GameManager.Instance.boxopened[a] == true)
                {
                    anim.SetTrigger("Open");
                }
                else
                {
                    Check();
                    CreateTextBox();
                    findDialog();
                }
                
            }
        }
        else
            Check();
            CreateTextBox();
            findDialog();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, seeRange);
    }
    public void SoundPlay(AudioClip audio)
    {
        SoundManager.instance.SFXPlay("gate", audio);
    }
}
