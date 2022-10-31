using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    //걍 leftProjectile 이 함수의 rigid값을 -로 하고 반대로
    public Rigidbody2D rigid;

    public Collider2D colliderDetected;
    public int dmg;
    public int stunDmg;
    public float speed = 3f;

    // Start is called before the first frame update
    private void Awake()
    {
        dmg = PlayerController.instance.rangeWeapon.dmg;
        stunDmg = PlayerController.instance.rangeWeapon.stunDmg;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rigid.velocity = transform.position.x > PlayerController.instance.transform.position.x ? new Vector2(speed, 0) : new Vector2(-speed, 0);
        Destroy(gameObject, 2.5f);
    }

    public void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = colliderDetected.GetComponent<EnemyController>();

            ProjectileAttack(enemy);

            Destroy(gameObject);
        }

        else if (colliderDetected.gameObject.CompareTag("Boss"))
        {
            BossGetDamage boss = colliderDetected.GetComponent<BossGetDamage>();

            ProjectileAttacktoBoss(boss);

            Destroy(gameObject);
        }

        if (colliderDetected.gameObject.CompareTag("Ground") || colliderDetected.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

    public void ProjectileAttack(EnemyController enemy)
    {
        enemy.GetDamage(dmg, stunDmg);
    }

    public void ProjectileAttacktoBoss(BossGetDamage boss)
    {
        boss.GetDamage(dmg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderDetected = collision;
        HitDetected();
    }

}
