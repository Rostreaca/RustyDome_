using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scene2QuestText : UIText
{
    public bool isquestclear;
    public Module module;
    public GameObject item;
    public GameObject Actor;
    public Text[] QuestEndText;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
        npc = GameObject.Find("NPC");
        npc_anim = npc.GetComponent<Animator>();
        
        Actor = GameObject.Find("Actor");
    }

    void Update()
    {
        FindNPC();
        isquestclear = GameManager.Instance.isQuestClear;
        CheckSayEnd();
        TextPosition(transform, dialog, npc, 1.5f);

        if (GameManager.Instance.Scene2MissonStart == true)
        {
            Say();
        }
    }

    new public void Say()
    {
        if (sayCount == 0 && GameManager.Instance.Scene2MissonClear != true)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = t1ext[0].text;
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.Scene2MissonClear != true)
        {
            PlayerController.instance.istalking = true;
            npc_Text = t1ext[sayCount + 1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount > 0 && sayCount != t1ext.Length && sayEnd == true && sayCount + 1 < t1ext.Length && GameManager.Instance.Scene2MissonClear != true)
        {
            npc_Text = t1ext[sayCount + 1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount + 1 == t1ext.Length && sayEnd == true && GameManager.Instance.Scene2MissonClear != true)
        {
            PlayerController.instance.istalking = false;
            sayCount = 0;
            npc_anim.SetBool("isTalking", false);
            dialog.SetActive(false);
        }

        if (sayCount == 0 && QuestManager.instance.Scene2enemycount == 0)
        {
            npc_anim.SetBool("isTalking", true);
            npc_Text = QuestEndText[0].text;
        }
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true && GameManager.Instance.Scene2MissonClear == true)
        {
            PlayerController.instance.istalking = true;
            npc_Text = QuestEndText[sayCount + 1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount > 0 && sayCount != t1ext.Length && sayEnd == true && sayCount + 1 < t1ext.Length && GameManager.Instance.Scene2MissonClear == true)
        {
            npc_Text = QuestEndText[sayCount + 1].text;
            Type_init();
            sayCount++;
        }
        if (Input.GetKey("f") && sayCount + 1 == t1ext.Length && sayEnd == true && GameManager.Instance.Scene2MissonClear == true)
        {
            PlayerController.instance.istalking = false;
            sayCount = 0;
            npc_anim.SetBool("isTalking", false);
            SceneManage.Instance.UpdownFadeIn(false);
            Invoke("LoadScene", 0.3f);
            dialog.SetActive(false);
        }

    }

    public void LoadScene()
    {
        SceneManage.Instance.InteractSceneLoad(3);
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
