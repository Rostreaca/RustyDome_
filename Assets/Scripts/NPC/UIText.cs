using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    public Animator npc_anim;
    public Text []t1ext;
    private IEnumerator init_Type;

    public GameObject dialog;
    public GameObject npc;
    public GameObject nextTextBtn;

    public Text text;

    public string npc_Text="'F'";


    public int sayCount = 0;
    public int textEnd=0;
    public bool sayEnd;

    public Vector2 originPos;

    // Start is called before the first frame update

    void Awake()
    {
        npc_anim = npc.GetComponent<Animator>();
        originPos = dialog.transform.position;
        init_Type = Typing();
    }
    
    void OnEnable()
    {
        Type_init();
    }
    void OnDisable()
    {
        sayCount = 0;
        npc_anim.SetBool("isTalking", false);
        dialog.transform.position = originPos;
    }
    // Update is called once per frame
    void Update()
    {
        CheckSayEnd();
        TextPosition(transform,dialog,npc,1f);
        Say();
    }

    public void Say()
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        } 
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)  
        {
            npc_anim.SetTrigger("Talk");
            npc_anim.SetBool("isTalking", true);
            npc_Text = t1ext[0].text;
            Type_init();
            sayCount ++;
        }     
        if (Input.GetKey("f")&&sayCount>0 && sayCount !=t1ext.Length && sayEnd == true)
            {
                npc_Text = t1ext[sayCount].text;
                Type_init();
                sayCount ++;
            }
            if (Input.GetKey("f") && sayCount == t1ext.Length && sayEnd == true)
            {
                npc_anim.SetBool("isTalking", false);
                dialog.SetActive(false);
            }
       
    }

    public void TextPosition(Transform transform, GameObject dialog, GameObject npc, float height)//대화창 위치를 캐릭터 머리 위로 조정
    {
        transform.position = Camera.main.WorldToScreenPoint(npc.transform.position + new Vector3(0, height, 0));
        dialog.transform.position = transform.position;
    }
    public void Type_init()//Typing() 코루틴 초기화
    {
        textEnd = 0;
        sayEnd = false;
        init_Type = Typing();
        StartCoroutine(init_Type);
    }

    public void CheckSayEnd()//대화가 끝나면 버튼 생성
    {
        if (sayEnd == true)
        {
            nextTextBtn.gameObject.SetActive(true);
        }
        else if (sayEnd != true)
        {
            nextTextBtn.gameObject.SetActive(false);
        }
    }

    public IEnumerator Typing()//글자가 한글자 씩 타이핑 되는 효과
    {
        for (int i = 0; i <= npc_Text.Length; i++)
        {
            text.text = npc_Text.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
            textEnd ++;
        }
        if(textEnd == npc_Text.Length+1)
        {
            sayEnd = true;
        }
    }
}
