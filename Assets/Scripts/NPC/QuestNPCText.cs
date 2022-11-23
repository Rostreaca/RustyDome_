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
            npc_Text = "�����༭ ����..";
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
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.isQuestClear == true) //�ϴ� �ӽ÷� killcount��� ����(�ƹ��� �۵�����) ������.
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = "����.. �̰� �����ϼ�..";

            Type_init();
            talkQuestClear = true;
            PlayerController.instance.scrap += 1500;//�̺κп� ��������� �߰�, �ٷ� ��ĭ���� �߰�? or �����۵��ó�� �ٴڿ� ����������
            Customize.instance.AddModule(module);
            Instantiate(item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity,Actor.transform);
            Debug.Log("a");
            sayCount++;

        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.isQuestClear != true)
        {
            GameManager.Instance.isQuestStart = true;
            npc_anim.SetBool("isTalking", true);
            npc_Text = "������ ������ \n ó���ϰ� ���ְ�..";
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
