using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigid;

    public Collider2D colliderDetected;
    public int projectiledmg = 60;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
        rigid = GetComponent<Rigidbody2D>();

        if (EnemyController.instance.transform.localScale==new Vector3(-1,1,1)) //플레이어가 왼쪽에 있을때
        {
            rigid.velocity = new Vector2(-speed, 0);
        }

        if (EnemyController.instance.transform.localScale == new Vector3(1, 1, 1)) //플레이어가 오른쪽에 있을때
        {
            rigid.velocity = new Vector2(speed, 0);
        }
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void HitDetected()
    {
        if (colliderDetected.gameObject.CompareTag("Player"))
        {
            PlayerController player = colliderDetected.GetComponent<PlayerController>();

            int damage = projectiledmg;

            ProjectileAttack(player, damage);

            Destroy(gameObject);
        }
    }

    public void ProjectileAttack(PlayerController player, int damage)
    {
        player.GetDamage(damage, transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colliderDetected = collision;
        HitDetected();
    }
}
