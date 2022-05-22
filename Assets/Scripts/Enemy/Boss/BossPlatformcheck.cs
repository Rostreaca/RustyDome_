using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatformcheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject != null && collision.gameObject.CompareTag("Ground"))
        {
            if(BossTest.instance.anim.GetBool("Pattern1LeftAtk")!=true)
            {
                BossTest.instance.saveTargetPos.y = collision.gameObject.transform.position.y;
            }
            else if(BossTest.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("BossAtkLeftRange"))
            {
                BossTest.instance.saveTargetPos.y = collision.gameObject.transform.position.y - 0.2f;
            }
        }
    }
}
