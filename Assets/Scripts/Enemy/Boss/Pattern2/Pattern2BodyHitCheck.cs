using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2BodyHitCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Ground")//플레이어가 트리거를 빠져나갔을 떄가 아닌 보스가 벽에 박았을 때 Pattern2start가 false가 되야함.
        {
            BossTest.instance.anim.SetBool("Pattern2start", false);
            BossTest.instance.anim.SetBool("Pattern2isCool", true);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Ground")//플레이어가 트리거를 빠져나갔을 떄가 아닌 보스가 벽에 박았을 때 Pattern2start가 false가 되야함.
        {
            BossTest.instance.anim.SetBool("Pattern2start", false);
            BossTest.instance.anim.SetBool("Pattern2isCool", true);
        }
    }

}
