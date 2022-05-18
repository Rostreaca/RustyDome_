using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2Check : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            if (BossTest.instance.isattack != true)
            {
                BossTest.instance.anim.SetBool("Pattern2start", true);
                BossTest.instance.anim.SetBool("Pattern1start", false);
                
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            if(BossTest.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("BossPattern2AtkPrePare")!=true)
                BossTest.instance.anim.SetBool("Pattern2start", false);

        }
    }

}
