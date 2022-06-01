using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPCText : UIText
{
    public bool questStart;
    public bool questClear;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        CheckSayEnd();
        TextPosition(transform, dialog, npc, 1.5f);

        if (questStart == false && QuestManager.instance.isallkilled == false)
        {
            Say();
        }
        else if (questStart == true && questClear == false || QuestManager.instance.isallkilled == true && questClear == false)
        {
            Quest();
        }
        else if(questClear == true)
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
            npc_anim.SetTrigger("Talk");
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
            npc_anim.SetTrigger("Talk");
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
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && QuestManager.instance.isallkilled == true) //�ϴ� �ӽ÷� killcount��� ����(�ƹ��� �۵�����) ������.
        {
            npc_anim.SetTrigger("Talk");
            npc_anim.SetBool("isTalking", true);
            npc_Text = "����.. �̰� �����ϼ�..";
            Type_init();
            questClear = true;
            PlayerController.instance.scrap += 1500;//�̺κп� ��������� �߰�, �ٷ� ��ĭ���� �߰�? or �����۵��ó�� �ٴڿ� ����������

            sayCount++;

        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && QuestManager.instance.isallkilled !=true)
        {
            npc_anim.SetTrigger("Talk");
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

}
