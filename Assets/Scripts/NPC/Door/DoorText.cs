using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorText : UIText
{

    public static DoorText instance;

    void singleton()
    {
        instance = this;
    }
    void Awake()
    {
        singleton();
        originPos = dialog.transform.position;
    }
    private void OnDisable()
    {
        sayCount = 0;
    }
    void Update()
    {
        FindNPC();
        CheckSayEnd();
        DoorInteract();
    }

    void DoorInteract()//fŰ�� ������ ��ȣ�ۿ�
    {
        if (sayCount == 0)
        {
            TextPosition(transform, dialog, npc, 2f);
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            dialog.SetActive(false);
        }
    }
}
