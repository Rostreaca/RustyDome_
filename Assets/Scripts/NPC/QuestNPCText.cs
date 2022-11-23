using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPCText : UIText
{
    public bool questStart;
    public bool talkQuestClear;
    public Module module;
    public GameObject item;
    public GameObject Actor;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
        Actor = GameObject.Find("Actor");
    }

    void Update()
    {
        CheckSayEnd();
        TextPosition(transform, dialog, npc, 1.5f);

        if (questStart == false && GameManager.Instance.isQuestClear == false)
        {
            Say();
        }
        else if (questStart == true && talkQuestClear == false || GameManager.Instance.isQuestClear == true && talkQuestClear == false)
        {
            Quest();
        }
        else if(talkQuestClear == true)
        {
            Thanks();
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
            npc_Text = "도와줘서 고맙네..";
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
            npc_Text = "'F'";
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
            questStart = true;
            sayCount = 0;
            Quest();
        }

    }
    public void Quest()
    {

        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.isQuestClear == true) //일단 임시로 killcount라는 변수(아무런 작동없음) 설정함.
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = "고맙네.. 이건 보상일세..";

            Type_init();
            talkQuestClear = true;
            PlayerController.instance.scrap += 1500;//이부분에 보상아이템 추가, 바로 템칸으로 추가? or 아이템드롭처럼 바닥에 떨어지게함
            Customize.instance.AddModule(module);
            Instantiate(item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity,Actor.transform);
            Debug.Log("a");
            sayCount++;

        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.isQuestClear != true)
        {
            GameManager.Instance.isQuestStart = true;
            npc_anim.SetBool("isTalking", true);
            npc_Text = "망가진 기계들을 \n 처리하고 와주게..";
            Type_init();

            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true)
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }
    }

    public override void talksound()
    {
        if (sayCount != 0)
        {
            int talk = Random.Range(0, 11);
            SoundManager.instance.SFXPlay("Talk", talkclip[talk]);
        }
    }
}
