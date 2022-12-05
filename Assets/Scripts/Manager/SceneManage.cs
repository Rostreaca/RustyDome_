using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    public bool Continue = false;
    public bool CameLeft;
    public GameObject RightFadeIn, LeftFadeIn , UpFadeIn, DownFadeIn;
    public static SceneManage Instance;
    public Scene nowscene;
    public int NextScene, PreScene;
    public GameObject NextPortal;
    public GameObject PrePortal;
    public GameObject InteractPortal;

    public float farfromportal = 3f;
    // Start is called before the first frame update

    public void Singleton_Init()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
        }
       // DontDestroyOnLoad(gameObject);
    }    
    void Start()
    {
    }
    private void Awake()
    {
        RightFadeIn = gameObject.transform.GetChild(0).gameObject;
        LeftFadeIn = gameObject.transform.GetChild(1).gameObject;
        UpFadeIn = gameObject.transform.GetChild(2).gameObject;
        DownFadeIn = gameObject.transform.GetChild(3).gameObject;
        Singleton_Init();
    }
    private void Update()
    {
        if(GameObject.Find("InteractScenePortal"))
        {
            InteractPortal = GameObject.Find("InteractScenePortal");
        }
        if (GameObject.Find("NextScenePortal"))
        {
            NextPortal = GameObject.Find("NextScenePortal");
        }
        if (GameObject.Find("PreScenePortal"))
        {
            PrePortal = GameObject.Find("PreScenePortal");
        }
        nowscene = SceneManager.GetActiveScene();
        NextScene = nowscene.buildIndex + 1;
        PreScene = nowscene.buildIndex - 1;
        if(PrePortal !=null&&NextPortal!=null)
        {
            if (PrePortal.transform.position.x > NextPortal.transform.position.x) // 이전맵으로 가는 포탈이 다음 맵으로 가는 포탈보다 오른쪽에 있는 경우
            {
                farfromportal = 3f;
            }
            else if (PrePortal.transform.position.x < NextPortal.transform.position.x)// 이전맵으로 가는 포탈이 다음 맵으로 가는 포탈보다 왼쪽에 있는 경우
            {
                farfromportal = -3f;
            }
        }
        else if(PrePortal !=null && NextPortal ==null) //Pre 포탈만 있는경우
        {
            if (nowscene.buildIndex == 2) // Pre 포탈이 오른쪽에 있는 맵
            {
                farfromportal = 3f;
            }
            else
            farfromportal = -3f;
        }
        else if (PrePortal == null && NextPortal != null) //Next 포탈만 있는경우
        {
            farfromportal = 3f;
        }
    }
    public void UpdownFadeIn(bool isUp)
    {
        if(isUp !=true)
        {
            DownFadeIn.SetActive(true);
        }
        if(isUp == true)
        {
            UpFadeIn.SetActive(true);
        }
    }
    public void FadeIn(bool isLeft)
    {
        if (isLeft !=true)
        {
            RightFadeIn.SetActive(true);
        }
        if(isLeft ==true)
        {
            LeftFadeIn.SetActive(true);
        }
    }
    public void FadeOut()
    {
        if (RightFadeIn.activeSelf == true)
        {
            RightFadeIn.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        if (LeftFadeIn.activeSelf == true)
        {
            LeftFadeIn.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        if (UpFadeIn.activeSelf == true)
        {
            UpFadeIn.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        if (DownFadeIn.activeSelf == true)
        {
            DownFadeIn.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        Invoke("EndLoad", 0.8f);
    }
    public void SaveSceneLoad()
    {
        Continue = true;
        //PlayerController.instance.transform.position = new Vector2(0f,0f); //DontDestoryOnLoad로 플레이어 포지션이 포탈과 겹치는 경우 문제가 발생하는 것 발견. 플레이어 포지션을 멀찍이 이동시킨후 SceneMover함수에서 재이동시킴.
        SceneManager.LoadScene(MenuManager.Instance.SceneIndex);

        DataManager data = GameObject.Find("DataManager").GetComponent<DataManager>();

        data.isGameLoaded = true;

        Invoke("NextSceneMover", 0.1f);
    }
    public void NextSceneLoad()
    {
        if (nowscene.buildIndex !=0)
        {
            PlayerController.instance.transform.position = new Vector2(0f, 0f);
        }

        if (nowscene.buildIndex == 4)
        {
            SceneManager.LoadScene(6);
        }
        else if (NextScene != 0)
        {
            SceneManager.LoadScene(NextScene);
        }
        Invoke("NextSceneMover", 0.1f);
    }
    public void NextSceneMover()
    {
        if(nowscene.buildIndex == 1&& Continue == false)
        {
            GameManager.Instance.isSave = true;
        }
        GameManager.Instance.NowLoading = true;
        if (PrePortal != null)
        {
            PlayerController.instance.transform.position = new Vector2(PrePortal.transform.position.x - farfromportal, PrePortal.transform.position.y); 
        }

        Invoke("FadeOut",0.5f);
    }
    public void PreSceneLoad()
    {
        PlayerController.instance.transform.position = new Vector2(0f, 0f);
        if (nowscene.buildIndex == 6)
        {
            CameLeft = false;
            SceneManager.LoadScene(4);
        }
        else if (nowscene.buildIndex == 5)
        {
            CameLeft = true;
            SceneManager.LoadScene(4);
        }
        else if (nowscene.buildIndex == 4)
        {
            SceneManager.LoadScene(5);
        }
        else if(PreScene != 0)
        {
            SceneManager.LoadScene(PreScene);
        }
        Invoke("PreSceneMover", 0.1f);
    }
    public void PreSceneMover()
    {
        GameManager.Instance.NowLoading = true;

        if (NextPortal != null && PrePortal != null && nowscene.buildIndex == 4)
        {
            if(CameLeft == true)
            {
                PlayerController.instance.transform.position = new Vector2(PrePortal.transform.position.x - farfromportal, PrePortal.transform.position.y);
            }
            else if(CameLeft == false)
            {
                PlayerController.instance.transform.position = new Vector2(NextPortal.transform.position.x + farfromportal, NextPortal.transform.position.y);
            }
        }
        else if (NextPortal != null)
        {
            PlayerController.instance.transform.position = new Vector2(NextPortal.transform.position.x + farfromportal, NextPortal.transform.position.y);
        }
        else if(NextPortal == null && PrePortal != null)
        {
            PlayerController.instance.transform.position = new Vector2(PrePortal.transform.position.x + farfromportal, PrePortal.transform.position.y);//NextPortal이 없기 때문에 farfromportal 값은 -3
        }
        Invoke("FadeOut", 0.5f);
    }

    public void InteractSceneLoad(int mapnumber)
    {
        SceneManager.LoadScene(mapnumber);

        Invoke("DoorSceneMover", 0.1f);
    }
    public void DoorSceneMover()
    {
        GameManager.Instance.NowLoading = true;
        if (InteractPortal !=null)
        {
            PlayerController.instance.transform.position = new Vector2(InteractPortal.transform.position.x, InteractPortal.transform.position.y);
        }

        Invoke("FadeOut", 0.5f);
    }
    public void EndLoad()
    {
        GameManager.Instance.NowLoading = false;
    }
}
