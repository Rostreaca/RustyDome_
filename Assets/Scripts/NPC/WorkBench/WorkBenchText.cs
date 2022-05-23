using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBenchText : UIText
{
    [Header("WorkBenchText���� �ʿ��� ��")]
    public Transform workBenchPos;
    PlayerController player;
    GameManager gm;
    void Awake()
    {
        originPos = dialog.transform.position;
        gm = GameManager.Instance;
        player = PlayerController.instance;
    }
    private void OnDisable()
    {
        WorkBenchController.instance.anim.SetBool("isOpen", false);
        sayCount = 0;
    }
    void Update()
    {
        CheckSayEnd();
        WorkBenchInteract();
    }

    void WorkBenchInteract()//fŰ�� ������ ��ȣ�ۿ�
    {
        if (sayCount == 0)
        {
            TextPosition(transform, dialog, npc, 0.2f);
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            dialog.transform.position = originPos;
            WorkBenchController.instance.anim.SetBool("isOpen", true);
            player.hpNow = player.hpMax; //ü�� ȸ��
            gm.checkPoint = workBenchPos.transform; //üũ����Ʈ ����
            sayCount = 1;
            
        }
    }


}
