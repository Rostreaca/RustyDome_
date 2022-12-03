using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneText : UIText
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Type_init();
    }
    // Update is called once per frame
    void Update()
    {
        FindNPC();
        TextPosition(transform, dialog, npc, 1f);
        CheckSayEnd();
        Say();
    }
    private void OnDisable()
    {
        //GameManager.Instance.RuinCutscenePlaying = false;
    }
    new public void Say()
    {
        if (sayCount == 0)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = t1ext[0].text;
        }
        if (sayCount == 0 && sayEnd == true)
        {
            npc_Text = t1ext[sayCount + 1].text;
            Type_init();
            sayCount++;
        }
        if (sayCount > 0 && sayCount != t1ext.Length && sayEnd == true && sayCount + 1 < t1ext.Length)
        {
            npc_Text = t1ext[sayCount + 1].text;
            Type_init();
            sayCount++;
        }
        if (sayCount + 1 == t1ext.Length && sayEnd == true)
        {
            sayCount = 0;
            RuinCutScene.instance.TalkEnd = true;
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);

        }

    }
}
