using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private bool playerCheck;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (playerCheck && Input.GetKey(KeyCode.S))
        {
            effector.rotationalOffset = 180f;

            Invoke("PlatformReset", 0.5f);
        }
        //else if (Input.GetKeyDown(KeyCode.W))
        //{
        //    effector.rotationalOffset = 0f;
        //}
    }

    public void PlatformReset()
    {
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
