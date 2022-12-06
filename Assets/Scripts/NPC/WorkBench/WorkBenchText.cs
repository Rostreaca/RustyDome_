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
        WorkBenchController.instance.anim.SetBool("isOpen", false);
        sayCount = 0;
        isopen = false; isopening = false;
    }
    bool isopening;
    void Update()
    {
        FindNPC();
        CheckSayEnd();
        WorkBenchInteract();
        if(!isopening)
        {
            OpenScreen();
        }
        if (UIManager.instance.customizeScreen.alpha == 0 && isopening)
        {
            PlayerController.instance.istalking = false;
        }
    }
    void OpenScreen()
    {
        if (WorkBenchController.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("WorkBench1_Open_Ani") || WorkBenchController.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("WorkBench2_Open_Ani") || WorkBenchController.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("WorkBench3_Open_Ani") || WorkBenchController.instance.anim.GetCurrentAnimatorStateInfo(0).IsName("WorkBench4_Open_Ani"))
        {
            if (WorkBenchController.instance.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                if (PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("Char_analyzing01"))
                {
                    UIManager.instance.ChangeScreen(UIManager.ScreenState.Customize);
                    Customize.instance.canCustomize = true;
                    isopening = true;
                }
            }
        }
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
            PlayerController.instance.istalking = true;
            WorkBenchController.instance.anim.SetBool("isOpen", true);
            //UIManager.instance.ChangeScreen(UIManager.ScreenState.Customize);
            //Customize.instance.canCustomize = true;

            dialog.GetComponent<CanvasGroup>().alpha = 0;
            sayCount = 1;
            dialog.transform.position = originPos;
            player.hpNow = player.hpMax; //체력 회복
            GameManager.Instance.checkPoint = workBenchPos.transform; //체크포인트 저장
            isopen = true;
            GameManager.Instance.isSave = true;
        }
    }


}
