using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBenchText : UIText
{
    public static WorkBenchText instance;

    [Header("WorkBenchText에서 필요한 값")]
    public Transform workBenchPos;
    PlayerController player;
    GameManager gm;
    bool isopen = false;
    void singleton()
    {
        instance = this;
    }
    void Awake()
    {
        workBenchPos = npc.transform;
        singleton();
        originPos = dialog.transform.position;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnDisable()
    {
        dialog.GetComponent<CanvasGroup>().alpha = 1;
        WorkBenchController.instance.canopenCustomize = false;
        WorkBenchController.instance.anim.SetBool("isOpen", false);
        sayCount = 0;
        if (isopen) { SoundManager.instance.SFXPlay("Door_Lock3", sfxclip[1]); }
        isopen = false;
    }
    void Update()
    {
        FindNPC();
        CheckSayEnd();
        WorkBenchInteract();
    }

    void WorkBenchInteract()//f키를 눌러서 상호작용
    {
        if (sayCount == 0)
        {
            TextPosition(transform, dialog, npc, 0.2f);
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            UIManager.instance.ChangeScreen(UIManager.ScreenState.Customize);
            dialog.GetComponent<CanvasGroup>().alpha = 0;
            sayCount = 1;
            WorkBenchController.instance.canopenCustomize = true;
            dialog.transform.position = originPos;
            WorkBenchController.instance.anim.SetBool("isOpen", true);
            player.hpNow = player.hpMax; //체력 회복
            GameManager.Instance.checkPoint = workBenchPos.transform; //체크포인트 저장
            isopen = true;
            GameManager.Instance.isSave = true;
        }
    }


}
