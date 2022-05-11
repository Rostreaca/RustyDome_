using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnetic : MonoBehaviour
{
    public Transform itemPos;
    public Transform playerPos;

    public bool isGrounded = false;
    private static float followRange = 0.3f;

    private void Awake()
    {
        playerPos = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if(isGrounded == true)
        follow();
    }
    void follow()
    {
        if (Vector2.Distance(transform.position, playerPos.position) <= followRange)
        {
            if (itemPos != null)
            {
                itemPos.position = Vector2.MoveTowards(itemPos.transform.position, playerPos.transform.position, 2 / 2.0f * Time.deltaTime);
            }
        }    
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,followRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject != null & collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
