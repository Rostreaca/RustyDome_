using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public int hpMax;
    public int hpNow;

    public bool isdead;
    public bool isItemdrop;

    public GameObject Item;
    public GameObject Actor;
    public Animator anim;

    public GameObject Door;
    DoorController doorcontrol;
    private void Singleton()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Door = GameObject.Find("BossRoomDoor").gameObject;
        doorcontrol = Door.GetComponent<DoorController>();
        Singleton();
    }
    private void Awake()
    {
        Actor = GameObject.Find("Actor");
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossDeath") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && isItemdrop == false)
        {
            Instantiate(Item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, Actor.transform);
            isItemdrop = true;
        }
    }

    public void Death()
    {
        if(isdead ==false)
        {
            isdead = true;
            anim.SetTrigger("Death");
        }
    }

    public void doorOpen()
    {
        doorcontrol.doorOpen = true;
    }
}
