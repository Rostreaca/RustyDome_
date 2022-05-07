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
            npc_Text = "���� �̰��� �ֹ��� Rivad �Դϴ�.";
            Type_init();
            sayCount = 1;
        }
        if(Input.GetKey("f")&& sayCount ==1 && sayEnd ==true)
        {
            npc_Text = "�̰��� �׽�Ʈ�� ��ȭ�Դϴ�. 0.999999 = 1�ΰ���?";
            Type_init();
            sayCount = 2;
        }
        if(Input.GetKey("f")&& sayCount ==2 && sayEnd == true)
        {
            npc_Text = "�� �̻� �� ��ȭ�� ������. �� ����.";
            Type_init();
            sayCount = 3;
        }
        if(Input.GetKey("f") && sayCount == 3 && sayEnd == true)
        {
            dialog.SetActive(false);
        }
    }
    void Type_init()//Typing() �ڷ�ƾ �ʱ�ȭ
    {
        textEnd = 0;
        sayEnd = false;
        init_Type = Typing();
        StartCoroutine(init_Type);
    }

    void CheckSayEnd()//��ȭ�� ������ ��ư ����
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

    IEnumerator Typing()//���ڰ� �ѱ��� �� Ÿ���� �Ǵ� ȿ��
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
