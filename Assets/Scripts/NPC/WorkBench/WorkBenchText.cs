using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBenchText : UIText
{
    [Header("WorkBenchText에서 필요한 값")]
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

    void WorkBenchInteract()//f키를 눌러서 상호작용
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_Text = "이 부분을 워크벤치 인터페이스로 대체";
            player.hpNow = player.hpMax; //체력 회복
            gm.checkPoint.position = workBenchPos.transform.position; //체크포인트 저장
            Type_init();
            sayCount = 1;
        }
    }

}
