using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public bool isquestZone;
    public int Enemycount;
    public bool isallkilled;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quest1EndCheck();
    }
    void Quest1EndCheck()
    {
        if(isquestZone == true && Enemycount ==0 )
        {
            isallkilled = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.tag == "Player")
        {
            isquestZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject != null&&collision.gameObject.tag == "Player")
        {
            isquestZone = false;
        }
    }


}
