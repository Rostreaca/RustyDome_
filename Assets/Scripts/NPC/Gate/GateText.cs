using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateText : UIText
{
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDisable()
    {
        sayCount = 0;
        dialog.transform.position = originPos;
    }
    // Update is called once per frame
    void Update()
    {
        FindNPC();
        if (Inventory.instance.Search(item) != true)
        {
            GameManager.Instance.HaveGateKey = false;
        }
        else if (Inventory.instance.Search(item) == true)
        {
            GameManager.Instance.HaveGateKey = true;
        }
        CheckSayEnd();
        if (!GameManager.Instance.isGateOpen)
        {
            CheckHasKey();
        }
        if(GameManager.Instance.isGateOpen)
        {
            EnterTheGate();
        }
    }
    public void CheckHasKey()
    {
        TextPosition(transform, dialog, npc, 1f);
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.HaveGateKey == false)
        {
            npc_Text = "���谡 �����ϴ�.";
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.HaveGateKey == true)
        {
            GameManager.Instance.isGateOpen = true;
            npc_anim.SetBool("GateOpen", true);// ������ �ٲ�� �ִϸ��̼� ����.
            Type_init();
            sayCount++;
            EnterTheGate();
        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true)
        {
            dialog.SetActive(false);
        }
    }
    public void EnterTheGate()
    {
        TextPosition(transform, dialog, npc, 1f);
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true)
        {
            npc_Text = "������ ���ðڽ��ϱ�?";
            Type_init();
            sayCount++;
        }

        if (Input.GetKey("f") && sayCount == 2 && sayEnd == true)
        {
            npc_Text = "���� ���� ����.";
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount == 3 && sayEnd == true)
        {
            dialog.SetActive(false);
        }
    }


}
