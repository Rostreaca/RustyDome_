using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public int Enemycount;
    public int Scene2enemycount;
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
        if(Enemycount >= 7)
        {
            GameManager.Instance.isQuestClear = true;
        }

        if(SceneManager.GetActiveScene().buildIndex == 2 && Scene2enemycount ==0)
        {

        }
    }

    


}
