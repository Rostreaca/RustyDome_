using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerChestText : UIText
{
    public bool Cantrade;
    public bool TradeFInish;
    public Module module;
    public Item item;

    GameObject chest;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        FindNPC();
        if (GameObject.Find("Chest"))
        {
            chest = GameObject.Find("Chest");
        }
        TextPosition(transform, dialog, npc, 1f);
        CheckSayEnd();
        if (Cantrade == false && !TradeFInish)
        {
            Say();
        }
        else if (Cantrade == true)
        {
            Trade();
        }
    }
    public override void talksound()
    {
        //if (sayCount != 0)
        {
            int talk = Random.Range(0, 11);
            SoundManager.instance.SFXPlay("Talk", talkclip[talk]);
        }
    }

    new public void Say()
    {
        if (sayCount == 0)
        {
            //npc_anim.SetBool("isTalking", true);
            npc_Text = "�� ���ڿ��� ��ȭ����� ����ִٳ�... ��ö 200���� ��������...";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            Cantrade = true;
            sayCount = 0;
            Type_init();
            Trade();
        }

    }
    void Trade()
    {
        if(TradeFInish)
        {
            chest.GetComponent<NPCController>().enabled = false;
        }
        if (sayCount == 0&& !TradeFInish)
        {
            //npc_anim.SetBool("isTalking", true);
            npc_Text = "��ڳ�? ('F')";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && PlayerController.instance.scrap >= 200 && !TradeFInish)
        {
            chest.GetComponent<Animator>().SetTrigger("Open");
            npc_Text = "����... ��������...";
            Type_init();

            TradeFInish = true;
            PlayerController.instance.scrap -= 200;

            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && PlayerController.instance.scrap < 200&& !TradeFInish)
        {
            npc_Text = "��ö�� �����ϳ�...";
            Type_init();

            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true)
        {
            //npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }
    }
}
