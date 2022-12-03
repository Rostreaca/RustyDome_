using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private EdgeCollider2D col;
    private PlatformEffector2D effector;
    private bool playerCheck;

    void Start()
    {
        col = GetComponent<EdgeCollider2D>();
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (playerCheck && Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine(RotationConvert());
        }
    }

    private IEnumerator RotationConvert()
    {
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(0.4f);
        effector.rotationalOffset = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCheck = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCheck = false;
    }
}
