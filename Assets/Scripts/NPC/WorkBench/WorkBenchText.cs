using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBenchText : UIText
{
    public Transform workBenchPos;
    PlayerController player;
    GameManager gm;

    void Update()
    {
        CheckSayEnd();
        TextPosition(transform, dialog, npc);
        WorkBenchInteract();
    }

    // Start is called before the first frame update
    void Awake()
    {
        gm = GameManager.Instance;
        player = PlayerController.instance;
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
            player.hpNow = player.hpMax;
            gm.checkPoint.position = workBenchPos.transform.position;
            Type_init();
            sayCount = 1;
        }
    }
}
