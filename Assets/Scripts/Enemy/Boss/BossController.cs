using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public int hpMax;
    public int hpNow;

    public GameObject Item;
    GameObject Actor;

    Animator anim;

    private void singleton()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        singleton();
    }
    private void Awake()
    {
        Actor = GameObject.Find("Actor");
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {

        Destroy(gameObject, 1f);
    }
}
