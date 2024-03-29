using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool playerCheck;

    void Update()
    {
        if (playerCheck && Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine(CharacterDown());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.CompareTag("Player"))
        {
            playerCheck = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.CompareTag("Player"))
        {
            playerCheck = false;
        }
    }

    private IEnumerator CharacterDown()
    {
        CapsuleCollider2D playercol = PlayerController.instance.GetComponent<CapsuleCollider2D>();
        CompositeCollider2D platformcol = GetComponent<CompositeCollider2D>(); 
        Physics2D.IgnoreCollision(playercol, platformcol);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playercol, platformcol,false);
    }
}
