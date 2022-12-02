using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinCutScene : MonoBehaviour
{
    public static RuinCutScene instance;
    public bool TalkEnd = false;
    bool EventAct;
    public GameObject Camera,NPC ,Dialog;
    // Start is called before the first frame update
    public void Singleton_Init()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance !=null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Singleton_Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(TalkEnd == false)
        {
            ActiveDialog();
        }
    }

    void ActiveDialog()
    {
        float a, b;
        a= Mathf.Abs(Camera.transform.position.x);
        if (Mathf.Abs(Camera.transform.position.x - NPC.transform.position.x) < 2 && Mathf.Abs(Camera.transform.position.y - NPC.transform.position.y) < 2)
        {
            Dialog.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject !=null && collision.gameObject.tag == "Player" && EventAct == false)
        {
            EventAct = true;
            GameManager.Instance.RuinCutscenePlaying = true;
        }
    }
}
