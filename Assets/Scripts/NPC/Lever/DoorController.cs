using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public static DoorController instance;
    public bool doorOpen;

    private Vector2 targetpos;
    private Vector2 Originpos;
    // Start is called before the first frame update
    public void Singleton_Init()
    {
        instance = this;
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        Originpos = transform.position;
        targetpos = new Vector2(transform.position.x, transform.position.y + 5f);
        Singleton_Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(doorOpen == true)
        {
            DoorOpen();
        }
        if(doorOpen == false)
        {
            DoorClose();
        }
    }

    void DoorOpen()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetpos, 4f * Time.deltaTime);
    }
    void DoorClose()
    {
        transform.position = Vector2.MoveTowards(transform.position, Originpos, 4f * Time.deltaTime);
    }
}
