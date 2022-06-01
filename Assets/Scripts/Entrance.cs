using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public Bound bound;
    Transform target;

    public void Start()
    {
        bound = GetComponentInParent<Bound>();
        target = transform.GetChild(0).transform;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bound.curEntrance = this;
        }
    }

    public void Entree()
    {
        CameraManager.instance.transform.position = new Vector3(target.position.x, target.position.y, CameraManager.instance.transform.position.z);
        PlayerController.instance.transform.position = target.position;
    }
}
