using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNPCText : UIText
{
    public override void talksound()
    {
        //if (sayCount != 0)
        {
            int talk = Random.Range(0, 11);
            SoundManager.instance.SFXPlay("Talk", talkclip[talk]);
        }
    }
}
