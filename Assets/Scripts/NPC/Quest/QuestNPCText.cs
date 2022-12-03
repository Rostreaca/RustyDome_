using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNPCText : UIText
{
    public bool isquestclear;
    public bool RepeatTalk;
    public GameObject QuestProgresstxt;
    public bool questStart;
    public Module module;
    public GameObject item;
    public GameObject Actor;
    public Text[] QuestingText, QuestEndText;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
           QuestProgresstxt = GameObject.Find("[UI]").transform.Find("Canvas").transform.Find("GameScreen").transform.GetChild(3).gameObject;
        Actor = GameObject.Find("Actor");
    }
    void OnDisable()
    {
        sayCount = 0;
        npc_anim.SetBool("isTalking", false);
        dialog.transform.position = originPos;
        if(itemdropped)
        {
            GameManager.Instance.RepeatTalk = true;
        }
    }
    void Update()
    {
        questStart = GameManager.Instance.isQuestStart;
        isquestclear = GameManager.Instance.isQuestClear; 
        CheckSayEnd();
        TextPosition(transform, dialog, npc, 1.5f);

            RepeatTalk = GameManager.Instance.RepeatTalk;

        if (questStart == false && GameManager.Instance.isQuestClear == false)
        {
            Say();
        }
        else if (questStart == true || GameManager.Instance.isQuestClear == true)
        {
            Quest();
        }
    }

    new public void Say()
    {
        if (sayCount == 0)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = t1ext[0].text;
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_Text = t1ext[sayCount+1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount > 0 && sayCount != t1ext.Length && sayEnd == true&& sayCount+1 < t1ext.Length)
        {
            npc_Text = t1ext[sayCount+1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount+1 == t1ext.Length && sayEnd == true)
        {
            sayCount = 0;
            GameManager.Instance.isQuestStart = true;
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }

    }
    public bool itemdropped;
    public void Quest()
    {
        if (sayCount == 0 && RepeatTalk == false)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = "음? 벌써 처리했나?";
        }
        if (sayCount == 0 && RepeatTalk == true && isquestclear != true) //퀘스트 진행 중 반복대사
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestingText[QuestingText.Length-1].text;
        }
        if (sayCount == 0 && RepeatTalk == true && isquestclear == true || sayCount == 0 && itemdropped == true) //퀘스트 완료 시 반복대사
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestEndText[QuestEndText.Length - 1].text;
        }
        if (Input.GetKey("f")&& sayCount == 0 && RepeatTalk == true && isquestclear == true || Input.GetKey("f") && sayCount == 0 && itemdropped == true) //퀘스트 완료 시 반복대사
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && RepeatTalk == true && isquestclear != true)
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && isquestclear == true && RepeatTalk == false && !itemdropped) // 퀘스트 완료 시 대사
        {
            npc_Text = QuestEndText[sayCount].text;

            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true && isquestclear == true && RepeatTalk == false && !itemdropped)
        {
            npc_Text = QuestEndText[sayCount].text;

            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 2 && sayEnd == true && isquestclear == true && RepeatTalk == false && !itemdropped)
        {
            npc_Text = QuestEndText[sayCount].text;

            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 3 && sayEnd == true && isquestclear == true && RepeatTalk == false && !itemdropped)
        {
            QuestManager.instance.completelyend = true; // 퀘스트 완료 텍스트 없애기
               itemdropped = true;
            npc_Text = QuestEndText[sayCount].text;
            Debug.Log("아이템드랍"); 
            Instantiate(item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, Actor.transform);
            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 4 && sayEnd == true && isquestclear == true && RepeatTalk == false)
        {
            GameManager.Instance.isSave = true;
            GameManager.Instance.RepeatTalk = true;
            //RepeatTalk = true;
            npc_Text = QuestEndText[QuestEndText.Length -1].text;
            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 5 && sayEnd == true && isquestclear == true)
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && isquestclear != true && RepeatTalk == false) // 퀘스트 미완료시 대사
        {
            GameManager.Instance.isQuestStart = true;
            npc_Text = QuestingText[0].text;
            Type_init();
            QuestProgresstxt.SetActive(true);
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true && isquestclear != true && RepeatTalk == false)
        {
            GameManager.Instance.isQuestStart = true;
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestingText[1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount == 2 && sayEnd == true && isquestclear != true && RepeatTalk == false)
        {
            GameManager.Instance.RepeatTalk = true;
            //RepeatTalk = true;//반복대사 트리거 on
            GameManager.Instance.isQuestStart = true;
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestingText[QuestingText.Length-1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount == 3 && sayEnd == true && isquestclear != true)
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }
    }

    public override void talksound()
    {
        //if (sayCount != 0)
        {
            int talk = Random.Range(0, 11);
            SoundManager.instance.SFXPlay("Talk", talkclip[talk]);
        }
    }
}
