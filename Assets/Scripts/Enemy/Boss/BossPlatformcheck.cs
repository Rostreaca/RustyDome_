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
                BossTest.instance.saveTargetPos.y = collision.gameObject.transform.position.y + 2.5f;
            }
            else
                BossTest.instance.saveTargetPos.y = collision.gameObject.transform.position.y + 2.0f;
        }
    }
}
