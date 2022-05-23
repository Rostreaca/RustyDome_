using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBenchController : NPCManager
{
    public static WorkBenchController instance;
    public Animator anim;
    public void SIngleton_Init()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SIngleton_Init();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Check();
        CreateTextBox();
    }
    public new void Check()
    {
        if (Vector2.Distance(new Vector2(transform.position.x,transform.position.y-0.5f), playerPos.position) < seeRange)
        {
            isSee = true;
        }
        else
            isSee = false;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x,transform.position.y -0.5f), seeRange);
    }
}
