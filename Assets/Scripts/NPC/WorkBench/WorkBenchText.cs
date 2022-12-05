using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkBenchText : UIText
{
    public static WorkBenchText instance;

    [Header("WorkBenchText���� �ʿ��� ��")]
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

    void WorkBenchInteract()//fŰ�� ������ ��ȣ�ۿ�
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
            player.hpNow = player.hpMax; //ü�� ȸ��
            GameManager.Instance.checkPoint = workBenchPos.transform; //üũ����Ʈ ����
            isopen = true;
            GameManager.Instance.isSave = true;
        }
    }


}
