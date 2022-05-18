using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1Check : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Player;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Boss.transform.position,Player.transform.position) <2f)
        {
            BossTest.instance.anim.SetInteger("HowFarFromBoss", -1);
        }
        else
        {
            BossTest.instance.anim.SetInteger("HowFarFromBoss", 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject != null && collision.gameObject.tag =="Player")
        {
            if (BossTest.instance.isattack != true)
            {
                BossTest.instance.anim.SetBool("Pattern1start",true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            if (BossTest.instance.isattack != true)
            {
                BossTest.instance.anim.SetBool("Pattern1start",false);
                BossTest.instance.anim.SetBool("Pattern1RightAtk", false);
                BossTest.instance.anim.SetBool("Pattern1MiddleAtk", false);
                BossTest.instance.anim.SetBool("Pattern1LeftAtk", false);
            }

        }
    }
}
