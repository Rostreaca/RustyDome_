using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestManager : MonoBehaviour
{
    public GameObject Merchant, Chest;
    public GameObject ProgressTxt;
    public static QuestManager instance;
    public int Enemycount;
    public int Scene2enemycount;

    public bool queststart;
    public bool completelyend;
    private void Awake()
    {
        if(instance == null)
        {

            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {

        if(GameObject.Find("GameScreen").transform.GetChild(3).gameObject)
        {
            ProgressTxt = GameObject.Find("GameScreen").transform.GetChild(3).gameObject;
        }
        if(GameManager.Instance.isQuestStart )
        {
            ProgressTxt.SetActive(true);
        }
        else if(completelyend)
            ProgressTxt.SetActive(false);

        if (Enemycount >= 7)
        {
            GameManager.Instance.isQuestClear = true;
        }
        if(SceneManager.GetActiveScene().buildIndex == 2 && Scene2enemycount ==0 && GameManager.Instance.Scene2MissonStart)
        {
            GameManager.Instance.Scene2MissonClear = true;
        }

        if (SceneManage.Instance.nowscene.buildIndex == 4 && completelyend)
        {
            spawnMerchant();
        }
    }
    public void spawnMerchant()
    {
        Merchant = GameObject.Find("Actor").transform.Find("Merchant").gameObject;
        Chest = GameObject.Find("Actor").transform.Find("Chest").gameObject;

        Merchant.SetActive(true);
        Chest.SetActive(true);
    }

    


}
