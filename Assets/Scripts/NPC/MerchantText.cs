using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MerchantText : UIText
{
    public bool Cantrade;
    public bool TradeFInish;
    public Module module;
    public Item item;
    public GameObject Bullets;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        TextPosition(transform, dialog, npc, 1f);
        CheckSayEnd();
        if(Cantrade ==false&&!TradeFInish)
        {
            Say();
        }
        else if(Cantrade == true &&!TradeFInish)
        {
            Trade();
        }
        //else if(Cantrade == false &&TradeFInish)
        //{
        //    Thanks();
        //}
    }
    public override void talksound()
    {
        //if (sayCount != 0)
        {
            int talk = Random.Range(0, 11);
            SoundManager.instance.SFXPlay("Talk", talkclip[talk]);
        }
    }

    public void Thanks()
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = "고맙네..";
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true)
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }
    }
    new public void Say()
    {
        if (sayCount == 0)
        {
            npc_Text = "아... 자네인가...";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = t1ext[0].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount > 0 && sayCount != t1ext.Length && sayEnd == true)
        {
            npc_Text = t1ext[sayCount].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount == t1ext.Length && sayEnd == true)
        {
            Cantrade = true;
            sayCount = 0;
            Type_init();
            Trade();
        }

    }
    void Trade()
    {
        if (sayCount == 0 )
        {
            npc_Text = "사겠나? ('F')";
        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && PlayerController.instance.scrap >= 50)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = "고맙네... 가져가게...";
            Type_init();

            //TradeFInish = true;
            //Cantrade = false;
            PlayerController.instance.scrap -= 50;
            for (int i = 1; i <= 5; i++)
            {
                float dropPos = Random.Range(-0.5f, 0.5f);

                Instantiate(Bullets, new Vector2(transform.position.x + dropPos, transform.position.y + 0.5f), Quaternion.identity);
            }

            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && PlayerController.instance.scrap < 50)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = "고철이 부족하네...";
            Type_init();

            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true)
        {
            sayCount = 0;
            Type_init();
        }
    }
}
