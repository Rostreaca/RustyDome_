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
    void Update()
    {
        CheckSayEnd();
        TextPosition(transform, dialog, npc,0.2f);
        WorkBenchInteract();
    }

    void WorkBenchInteract()//fŰ�� ������ ��ȣ�ۿ�
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_Text = "�� �κ��� ��ũ��ġ �������̽��� ��ü";
            player.hpNow = player.hpMax; //ü�� ȸ��
            gm.checkPoint.position = workBenchPos.transform.position; //üũ����Ʈ ����
            Type_init();
            sayCount = 1;
        }
    }

}
