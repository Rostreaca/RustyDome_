using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : NPCManager
{
    // Update is called once per frame

    private void Awake()
    {
    }
    void Update()
    {
        findDialog();
        Check();
        CreateTextBox();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, seeRange);
    }
}
