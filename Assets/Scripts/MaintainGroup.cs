using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainGroup : MonoBehaviour
{
    public static MaintainGroup instance;

    private void Singleton_Init()
    {
        if(instance == null)
        {
            instance = this;
        }    
        else if(instance !=null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        Singleton_Init();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.Find("UIManager"))
        {

            UIManager uimanager = gameObject.transform.Find("UIManager").GetComponent<UIManager>();
            if (SceneManage.Instance.nowscene.buildIndex == 0)
            {
                uimanager.enabled = false;
            }
            else
                uimanager.enabled = true;
        }

    }
}
