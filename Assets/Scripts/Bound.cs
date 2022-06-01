using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    BoxCollider2D col;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraManager.instance.SetBound(col);
        }
    }
}
