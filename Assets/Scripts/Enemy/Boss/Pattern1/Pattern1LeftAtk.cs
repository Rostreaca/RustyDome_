using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1LeftAtk : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            if (BossTest.instance.isattack != true)
            {
                BossTest.instance.anim.SetBool("Pattern1BodySideAtk", false);
                BossTest.instance.anim.SetBool("Pattern1RightAtk", false);
                BossTest.instance.anim.SetBool("Pattern1MiddleAtk", false);
                BossTest.instance.anim.SetBool("Pattern1LeftAtk", true);
            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            if (BossTest.instance.isattack != true)
            {
                BossTest.instance.anim.SetBool("Pattern1BodySideAtk", false);
                BossTest.instance.anim.SetBool("Pattern1RightAtk", false);
                BossTest.instance.anim.SetBool("Pattern1MiddleAtk", false);
                BossTest.instance.anim.SetBool("Pattern1LeftAtk", true);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            if (BossTest.instance.isattack != true)
            {
                BossTest.instance.anim.SetBool("Pattern1LeftAtk", false);
            }

        }
    }
}
