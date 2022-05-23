using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{

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
        NPCController.instance.anim.SetBool("isTalking", false);
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
            NPCController.instance.anim.SetTrigger("Talk");
            NPCController.instance.anim.SetBool("isTalking", true);
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
            NPCController.instance.anim.SetBool("isTalking",false);
            dialog.SetActive(false);
        }
    }

    public void TextPosition(Transform transform, GameObject dialog, GameObject npc, float height)//��ȭâ ��ġ�� ĳ���� �Ӹ� ���� ����
    {
        transform.position = Camera.main.WorldToScreenPoint(npc.transform.position + new Vector3(0, height, 0));
        dialog.transform.position = transform.position;
    }
    public void Type_init()//Typing() �ڷ�ƾ �ʱ�ȭ
    {
        textEnd = 0;
        sayEnd = false;
        init_Type = Typing();
        StartCoroutine(init_Type);
    }

    public void CheckSayEnd()//��ȭ�� ������ ��ư ����
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

    public IEnumerator Typing()//���ڰ� �ѱ��� �� Ÿ���� �Ǵ� ȿ��
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
