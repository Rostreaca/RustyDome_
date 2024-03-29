using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnetic : MonoBehaviour
{
    public static ItemMagnetic instance;

    public Transform itemPos;
    public Transform playerPos;

    public bool CanFollow = true;
    public bool isGrounded = false;
    public float followRange = 1.5f;

    void Singleton_Init()
    {
        instance = this;
    }

    private void OnDisable()
    {
        isGrounded = false;
    }

    private void Awake()
    {
        Singleton_Init();
        playerPos = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if(isGrounded == true && CanFollow)
        follow();
    }
    void follow()
    {
        if (Vector2.Distance(transform.position, playerPos.position) <= followRange)
        {
            if (itemPos != null)
            {
                itemPos.position = Vector2.MoveTowards(itemPos.transform.position, playerPos.transform.position, 3 / 2.0f * Time.deltaTime);
            }
        }    
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,followRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject != null & collision.gameObject.CompareTag("Ground") || collision.gameObject != null & collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
