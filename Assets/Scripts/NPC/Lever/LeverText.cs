using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverText : UIText
{
    public Item item;
    public bool isopen;
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
        CheckSayEnd();
        if(!isopen)
        {
            CheckHasHandle();
        }
    }

    public void CheckHasHandle()
    {
        TextPosition(transform, dialog, npc, 1f);
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true &&Inventory.instance.Search(item) != true)
        {
            npc_Text = "손잡이가 없는 레버다.";
            Type_init();
            sayCount++;
        }
        if(Input.GetKey("f") && sayCount == 0 && sayEnd == true && Inventory.instance.Search(item) == true)
        {
            isopen = true;
            npc_anim.SetBool("HandleWork", true);// 레버가 바뀌는 애니메이션 실행.
            Type_init();
            sayCount++;
            dialog.SetActive(false);
        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true)
        {
            dialog.SetActive(false);
        }
    }


}
