using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    public GameObject RightFadeIn, LeftFadeIn , UpFadeIn, DownFadeIn;
    public static SceneManage Instance;
    public Scene nowscene;
    public int NextScene, PreScene;
    public GameObject NextPortal;
    public GameObject PrePortal;

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
        DontDestroyOnLoad(gameObject);
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
            if (PrePortal.transform.position.x > NextPortal.transform.position.x)
            {
                farfromportal = 3f;
            }
            else if (PrePortal.transform.position.x < NextPortal.transform.position.x)
            {
                farfromportal = -3f;
            }
        }
        else if(PrePortal !=null && NextPortal ==null)
        {
            farfromportal = -3f;
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
    }
    public void SaveSceneLoad()
    {

        SceneManager.LoadScene(MenuManager.Instance.SceneIndex);

        DataManager data = GameObject.Find("DataManager").GetComponent<DataManager>();

        data.isGameLoaded = true;

        Invoke("NextSceneMover", 0.1f);
    }
    public void NextSceneLoad()
    {
        if (nowscene.buildIndex == 4)
        {
            SceneManager.LoadScene(NextScene + 1);
        }
        else if (NextScene != 0)
        {
            SceneManager.LoadScene(NextScene);
        }
        Invoke("NextSceneMover", 0.1f);
    }
    public void NextSceneMover()
    {
        if (PrePortal != null)
        {
            PlayerController.instance.transform.position = new Vector2(PrePortal.transform.position.x - farfromportal, PrePortal.transform.position.y);
            
        }

        Invoke("FadeOut",0.5f);
    }
    public void PreSceneLoad()
    {
        SceneManager.LoadScene(PreScene);
        Invoke("PreSceneMover", 0.1f);
    }
    public void PreSceneMover()
    {
        if (NextPortal != null)
        {
            PlayerController.instance.transform.position = new Vector2(NextPortal.transform.position.x + farfromportal, NextPortal.transform.position.y);
        }

        Invoke("FadeOut", 0.5f);
    }
}
