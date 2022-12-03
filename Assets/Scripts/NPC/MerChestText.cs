using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerChestText : UIText
{
    public bool Cantrade;
    public bool TradeFInish;

    public GameObject Drop_Item;
    public GameObject player;
    GameObject chest;

    public float seta = 80f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        FindNPC();
        if (npc.GetComponent<NPCController>().Item != null)
        {
            Drop_Item = npc.GetComponent<NPCController>().Item;
        }
        if (GameObject.Find("Chest"))
        {
            chest = GameObject.Find("Chest");
        }
        player = GameObject.FindWithTag("Player");
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
            npc_Text = "그 상자에는 강화모듈이 들어있다네... 고철 200개에 가져가게...";
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
            npc_Text = "사겠나? ('F')";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && PlayerController.instance.scrap >= 200 && !TradeFInish)
        {
            chest.GetComponent<Animator>().SetTrigger("Open");
            npc_Text = "고맙네... 가져가게...";
            Type_init();
            if (chest.transform.position.x > player.transform.position.x && seta < 90) { seta += 20; }
            else if (chest.transform.position.x < player.transform.position.x && seta > 90) { seta -= 20; }
            Instantiate(Drop_Item, new Vector2(chest.transform.position.x, chest.transform.position.y + 0.7f), Quaternion.identity);
            TradeFInish = true;
            PlayerController.instance.scrap -= 200;

            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && PlayerController.instance.scrap < 200&& !TradeFInish)
        {
            npc_Text = "고철이 부족하네...";
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
