using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverEvent : MonoBehaviour
{
    public GameObject door;
    DoorController doorcontrol;
    // Start is called before the first frame update
    void Start()
    {
        doorcontrol = door.GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DoorOpen()
    {
        doorcontrol.doorOpen = true;
    }
}
