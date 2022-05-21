using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGetDamage : MonoBehaviour
{
    BossController boss;
    private bool isdie = false;
    // Start is called before the firs

    // Update is called once per frame
    void Update()
    {
        boss = BossController.instance;
    }

    public void GetDamage(int damage)
    {
        boss.hpNow -= damage;

        if (boss.hpNow <= 0&&isdie == false)
        {
            boss.Death();
            isdie = true;
        }
    }
}
