using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEvent : MonoBehaviour
{
    public GameObject PlayerUI;
    public GameObject hpbar;
    public GameObject bossroomdoor;

    public int Checking_Cutscene;

    DoorController door;
    public bool Playerhit;
    // Start is called before the first frame update
    void Start()
    {
        door = bossroomdoor.GetComponent<DoorController>();
        PlayerUI = GameObject.Find("GameScreen");
        hpbar = GameObject.Find("[UI]").transform.Find("Canvas").transform.Find("GameScreen").transform.Find("Boss HP Bar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isBossDead == true)
        {
            Destroy(gameObject);
        }
        if (BossTest.instance !=null)
        {
            if (BossController.instance.isdead != true)
            {
                FollowDetected();

            } 
        }
        if(BossController.instance !=null && BossController.instance.isdead == true)
        {
            hpbar.SetActive(false);
        }
    }

    public void FollowDetected()
    {
            if (BossTest.instance.anim.GetBool("Pattern1start") == false && BossTest.instance.anim.GetBool("Pattern2start") == false && Playerhit == true)
            {
                BossTest.instance.follow();
            }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject!=null&&collision.gameObject.name == ("BossRoomTrigger"))
        {
            if (BossController.instance.isdead != true)
            {
                if(Checking_Cutscene <1)
                {
                    GameManager.Instance.BossCutscenePlaying = true;
                    Checking_Cutscene++;
                }

                Anime_boss.instance.playerentered = true;

                GameManager.Instance.isDoorOpen = false;
                if(GameManager.Instance.BossCutscenePlaying == false)
                {
                    PlayerUI.SetActive(true);
                    hpbar.SetActive(true);
                }
                if (GameManager.Instance.BossCutscenePlaying == true)
                {
                    PlayerUI.SetActive(false);
                    
                }
            }
            
            Playerhit = true;

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
