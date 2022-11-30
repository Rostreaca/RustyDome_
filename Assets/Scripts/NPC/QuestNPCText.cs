using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNPCText : UIText
{
    public bool isquestclear;
    public bool QuestingTalk;
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

    void Update()
    {
        questStart = GameManager.Instance.isQuestStart;
        isquestclear = GameManager.Instance.isQuestClear; 
        CheckSayEnd();
        TextPosition(transform, dialog, npc, 1.5f);

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
    public void Quest()
    {

        if (sayCount == 0 && QuestingTalk == false)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = "��? ���� ó���߳�?";
        }
        if (sayCount == 0 && QuestingTalk == true && isquestclear != true) //����Ʈ ���� �� �ݺ����
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestingText[QuestingText.Length-1].text;
        }
        if (sayCount == 0 && QuestingTalk == true && isquestclear == true) //����Ʈ ���� �� �ݺ����
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestEndText[QuestEndText.Length - 1].text;
        }
        if (Input.GetKey("f")&& sayCount == 0 && QuestingTalk == true && isquestclear == true) //����Ʈ ���� �� �ݺ����
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && QuestingTalk == true && isquestclear != true)
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && isquestclear == true && QuestingTalk == false) // ����Ʈ �Ϸ� �� ���
        {
            npc_Text = QuestEndText[sayCount].text;

            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true && isquestclear == true && QuestingTalk == false)
        {
            npc_Text = QuestEndText[sayCount].text;

            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 2 && sayEnd == true && isquestclear == true && QuestingTalk == false)
        {
            npc_Text = QuestEndText[sayCount].text;

            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 3 && sayEnd == true && isquestclear == true && QuestingTalk == false)
        {
            npc_Text = QuestEndText[sayCount].text;
            Debug.Log("�����۵��"); 
            Instantiate(item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, Actor.transform);
            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 4 && sayEnd == true && isquestclear == true && QuestingTalk == false)
        {
            GameManager.Instance.isSave = true;
            QuestingTalk = true;
            npc_Text = QuestEndText[QuestEndText.Length -1].text;
            Type_init();
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 5 && sayEnd == true && isquestclear == true)
        {
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }

        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && isquestclear != true && QuestingTalk == false) // ����Ʈ �̿Ϸ�� ���
        {
            GameManager.Instance.isQuestStart = true;
            npc_Text = QuestingText[0].text;
            Type_init();
            QuestProgresstxt.SetActive(true);
            sayCount++;

        }
        if (Input.GetKey("f") && sayCount == 1 && sayEnd == true && isquestclear != true && QuestingTalk == false)
        {
            GameManager.Instance.isQuestStart = true;
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestingText[1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount == 2 && sayEnd == true && isquestclear != true && QuestingTalk == false)
        {
            QuestingTalk = true;//�ݺ���� Ʈ���� on
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
