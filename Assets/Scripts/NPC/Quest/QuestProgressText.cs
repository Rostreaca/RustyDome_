using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestProgressText : MonoBehaviour
{
    public Text progressTxt;

    private void OnEnable()
    {
        Invoke("DisableCounter", 1.5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        progressTxt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isQuestStart == true)
        {
            if (GameManager.Instance.isQuestClear == false)
            {
                progressTxt.text = "퀘스트 진행도( " + QuestManager.instance.Enemycount + " / 7 )";
            }
            else if (GameManager.Instance.isQuestClear == true)
            {
                progressTxt.text = "퀘스트 완료!";
                GameManager.Instance.isQuestStart = false;
            }
        }
    }

    public void DisableCounter()
    {
        gameObject.SetActive(false);
    }

}
