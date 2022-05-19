using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2BodyHitCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Ground")//�÷��̾ Ʈ���Ÿ� ���������� ���� �ƴ� ������ ���� �ھ��� �� Pattern2start�� false�� �Ǿ���.
        {
            BossTest.instance.anim.SetBool("Pattern2start", false);
            BossTest.instance.anim.SetBool("Pattern2isCool", true);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Ground")//�÷��̾ Ʈ���Ÿ� ���������� ���� �ƴ� ������ ���� �ھ��� �� Pattern2start�� false�� �Ǿ���.
        {
            BossTest.instance.anim.SetBool("Pattern2start", false);
            BossTest.instance.anim.SetBool("Pattern2isCool", true);
        }
    }

}
