using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : NPCManager
{
    // Update is called once per frame

    void Update()
    {
        Check();
        CreateTextBox();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, seeRange);
    }
}
