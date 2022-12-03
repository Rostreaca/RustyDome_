using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxText : UIText
{
    public static BoxText instance;
    // Start is called before the first frame update
    [Header("BoxText에서 필요한 값")]
    public GameObject Actor;
    public GameObject Item;
    public GameObject Box;
    public GameObject player;

    public float seta = 80f;
    public bool isBoxOpen = false;
    void Awake()
    {
        originPos = dialog.transform.position;
        Singleton_Init();
    }
    void Singleton_Init()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        FindNPC();
        if (isBoxOpen == false)
        {
            CheckSayEnd();
            TextPosition(transform, dialog, npc,0.2f);
            BoxInteract();
        }
        else
            dialog.SetActive(false);
    }

    void BoxInteract()
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            Type_init();
            if (Box.transform.position.x > player.transform.position.x && seta < 90) { seta += 20; }
            else if (Box.transform.position.x < player.transform.position.x && seta > 90) { seta -= 20; }
            Instantiate(Item, new Vector2(Box.transform.position.x, Box.transform.position.y+0.01f), Quaternion.identity, Actor.transform);
            sayCount = 1;
            isBoxOpen = true;
            dialog.SetActive(false);
        }
    }
}
