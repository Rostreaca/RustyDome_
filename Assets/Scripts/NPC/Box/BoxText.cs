using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxText : UIText
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSayEnd();
        TextPosition(transform, dialog, npc);
        BoxInteract();
    }

    void BoxInteract()
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_Text = "이 부분을 아이템 뱉어내는것으로 대체";
            Type_init();
            sayCount = 1;
        }
    }
}
