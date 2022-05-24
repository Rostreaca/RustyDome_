using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    //걍 leftProjectile 이 함수의 rigid값을 -로 하고 반대로

    public static PlayerProjectile instance;
    public Rigidbody2D rigid;

    public Collider2D colliderDetected;
    public int projectiledmg;
    public float speed = 3f;

    void singleton()
    {
        instance = this;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        projectiledmg = PlayerController.instance.rangeWeapon.dmg;
        rigid = GetComponent<Rigidbody2D>();
        singleton();
    }
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = colliderDetected.GetComponent<EnemyController>();

            int damage = projectiledmg;

            ProjectileAttack(enemy, damage);

            Destroy(gameObject);
        }
        if (colliderDetected.gameObject.CompareTag("Ground") || colliderDetected.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

    public void ProjectileAttack(EnemyController enemy, int damage)
    {
        enemy.GetDamage(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderDetected = collision;
        HitDetected();
    }

}
