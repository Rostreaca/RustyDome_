using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    public static SceneManage Instance;
    public Scene nowscene;
    public int NextScene, PreScene;
    public GameObject NextPortal;
    public GameObject PrePortal;
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
        Singleton_Init();
    }
    private void Update()
    {
        if (NextPortal = GameObject.Find("NextScenePortal"))
        {
            NextPortal = GameObject.Find("NextScenePortal");
        }
        if (PrePortal = GameObject.Find("PreScenePortal"))
        {
            PrePortal = GameObject.Find("PreScenePortal");
        }
        nowscene = SceneManager.GetActiveScene();
        NextScene = nowscene.buildIndex + 1;
        PreScene = nowscene.buildIndex - 1;
    }
    public void NextSceneLoad()
    {
        if(NextScene != 0)
        {
            SceneManager.LoadScene(NextScene);
        }    
        Invoke("NextSceneMover", 0.1f);
    }
    public void NextSceneMover()
    {
        PlayerController.instance.transform.position = new Vector2(PrePortal.transform.position.x -3f, PrePortal.transform.position.y);
    }
    public void PreSceneLoad()
    {
        SceneManager.LoadScene(PreScene);
        Invoke("PreSceneMover", 0.1f);
    }
    public void PreSceneMover()
    {
        PlayerController.instance.transform.position = new Vector2(NextPortal.transform.position.x + 3f, NextPortal.transform.position.y);
    }
}
