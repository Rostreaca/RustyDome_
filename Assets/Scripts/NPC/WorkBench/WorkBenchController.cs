using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBenchController : NPCManager
{
    // Start is called before the first frame update
    void Start()
    {
    }

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
