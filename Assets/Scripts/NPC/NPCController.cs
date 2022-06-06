using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : NPCManager
{
    public static NPCController instance;

    public Animator anim;

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
        Check();
        CreateTextBox();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, seeRange);
    }

}
