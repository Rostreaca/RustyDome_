using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public static DoorController instance;
    public float doorspeed = 7f;
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
        if (GameManager.Instance.isDoorOpen == true)
        {
            DoorOpen();
        }
        if(GameManager.Instance.isDoorOpen == false)
        {
            DoorClose();
        }
    }

    void DoorOpen()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetpos, doorspeed * Time.deltaTime);
    }
    void DoorClose()
    {
        transform.position = Vector2.MoveTowards(transform.position, Originpos, doorspeed * Time.deltaTime);
    }
}
