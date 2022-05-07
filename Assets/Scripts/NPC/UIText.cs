using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{

    private IEnumerator init_Type;
    private int sayCount=0;

    public GameObject dialog;
    public GameObject npc;
    public GameObject nextTextBtn;
    public Text text;
    private string npc_Text="'F'";
    public int textEnd=0;
    public bool sayEnd;

    // Start is called before the first frame update

    void Awake()
    {
        init_Type = Typing();
    }
    
    void OnEnable()
    {
        Type_init();
    }
    void OnDisable()
    {
        sayCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        CheckSayEnd();
        Say();
    }

    void Say()
    {
        if (sayCount == 0)
        {
            npc_Text = "'F'";
        }
        transform.position = Camera.main.WorldToScreenPoint(npc.transform.position + new Vector3(0, 0.2f, 0));
        dialog.transform.position = transform.position;
        if (Input.GetKey("f") && sayCount == 0 && sayEnd == true)
        {
            npc_Text = "저는 이곳의 주민인 Rivad 입니다.";
            Type_init();
            sayCount = 1;
        }
        if(Input.GetKey("f")&& sayCount ==1 && sayEnd ==true)
        {
            npc_Text = "이것은 테스트용 대화입니다. 0.999999 = 1인가요?";
            Type_init();
            sayCount = 2;
        }
        if(Input.GetKey("f")&& sayCount ==2 && sayEnd == true)
        {
            npc_Text = "더 이상 할 대화가 없군요. 잘 가요.";
            Type_init();
            sayCount = 3;
        }
        if(Input.GetKey("f") && sayCount == 3 && sayEnd == true)
        {
            dialog.SetActive(false);
        }
    }
    void Type_init()//Typing() 코루틴 초기화
    {
        textEnd = 0;
        sayEnd = false;
        init_Type = Typing();
        StartCoroutine(init_Type);
    }

    void CheckSayEnd()//대화가 끝나면 버튼 생성
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

    IEnumerator Typing()//글자가 한글자 씩 타이핑 되는 효과
    {
        for (int i = 0; i <= npc_Text.Length; i++)
        {
            text.text = npc_Text.Substring(0, i);
            yield return new WaitForSeconds(0.2f);
            textEnd ++;
        }
        if(textEnd == npc_Text.Length+1)
        {
            sayEnd = true;
        }
    }
}
