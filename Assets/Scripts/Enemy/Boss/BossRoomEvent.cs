using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEvent : MonoBehaviour
{
    public GameObject hpbar;
        
    public bool Playerhit;
    // Start is called before the first frame update
    void Start()
    {
        hpbar = GameObject.Find("[UI]").transform.Find("Canvas").transform.Find("GameScreen").transform.Find("Boss HP Bar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(BossController.instance.isdead !=true)
        {
            FollowDetected();
        }
    }

    public void FollowDetected()
    {
            if (BossTest.instance.anim.GetBool("Pattern1start") == false && BossTest.instance.anim.GetBool("Pattern2start") == false&&Playerhit ==  true)
            {
                BossTest.instance.follow();
            }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject!=null&&collision.gameObject.name == ("BossRoomTrigger"))
        {
            Playerhit = true;

            hpbar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject != null && collision.gameObject.name==("BossRoomTrigger"))
        {
            Playerhit = false;


            hpbar.SetActive(false);
        }
    }
}
