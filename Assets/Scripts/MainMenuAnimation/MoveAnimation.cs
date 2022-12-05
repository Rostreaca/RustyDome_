using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    public float movespeed;
    public Vector3 startPos = new Vector3(-1560, 445, 0);
    public Vector3 targetPos = new Vector3(1320, 445, 0);

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, movespeed);

        if (transform.localPosition.x >= targetPos.x)
            transform.localPosition = startPos;
    }
}
