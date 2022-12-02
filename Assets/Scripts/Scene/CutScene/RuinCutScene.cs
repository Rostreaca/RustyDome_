using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinCutScene : MonoBehaviour
{
    EnemyController enemy;
    public static RuinCutScene instance;
    public bool TalkEnd = false;
    bool EventAct;
    public GameObject Camera,NPC ,Dialog , AttackEnemy ,LadderBound;
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
        enemy = AttackEnemy.GetComponent<EnemyController>();
        Camera = GameObject.Find("Main Camera");
        NPC = GameObject.Find("NPC");
        Singleton_Init();
    }

    bool Active1time = false;
    // Update is called once per frame
    void Update()
    {
        if(TalkEnd == false && (int)Camera.transform.position.y == (int)NPC.transform.position.y)
        {
            ActiveDialog();
        }
        if(TalkEnd == true&& Active1time == false && (int)Camera.transform.position.y == (int)LadderBound.transform.position.y)
        {
            EnemyMove();
            Active1time = true;
        }

        if(enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack")&& enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
        {
            GameManager.Instance.Scene2MissonStart = true;
            GameManager.Instance.RuinCutscenePlaying = false;
        }
    }

    void EnemyMove()
    {
            enemy.animator.SetTrigger("MeleeAttack");
    }
    void ActiveDialog()
    {
            Dialog.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject !=null && collision.gameObject.tag == "Player" && EventAct == false)
        {
            EventAct = true;
            if(GameManager.Instance.Scene2MissonStart == false)
            {
                GameManager.Instance.RuinCutscenePlaying = true;
            }
        }
    }
}
