using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    BoxCollider2D col;
    Transform target;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();   
        target = transform.GetChild(0).GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraManager.instance.SetBound(col);
            CameraManager.instance.transform.position = new Vector3(target.position.x, target.position.y, CameraManager.instance.transform.position.z);
            PlayerController.instance.transform.position = target.position;
        }
    }
}
