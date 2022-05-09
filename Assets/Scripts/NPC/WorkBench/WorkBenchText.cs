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

    void WorkBenchInteract()//f키를 눌러서 상호작용
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_Text = "이 부분을 워크벤치 인터페이스로 대체";
            player.hpNow = player.hpMax;
            gm.checkPoint.position = workBenchPos.transform.position;
            Type_init();
            sayCount = 1;
        }
    }
}
