using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    string SavePath;
    public string filename;
    Scene Menuscene;
    public static MenuManager Instance;

    public int SceneIndex;
    public GameObject MainMenu, OptionMenu;
    public Button StartButton,OptionButton,EndButton,BackButton,ContinueButton;

    public void Singleton_Init()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SavePath = Application.persistentDataPath + "/saves/";
        filename = SavePath + "Main" + "savedata.json";
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }
        Menuscene = SceneManager.GetActiveScene();
        Singleton_Init();
        StartButton.onClick.AddListener(GameStart);
        OptionButton.onClick.AddListener(Option);
        EndButton.onClick.AddListener(GameEnd);
        BackButton.onClick.AddListener(OptionBack);
        if(File.Exists(filename))
        {
            ContinueButton.onClick.AddListener(Continue);
        }
        else if(!File.Exists(filename))
        {
            ContinueButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GameStart()
    {
        Debug.Log("게임시작");
        SceneManage.Instance.UpdownFadeIn(true);
        Invoke("LoadStartScene", 0.3f);
    }

    public void LoadStartScene()
    {
        SceneManage.Instance.NextSceneLoad();
    }
    public void Option()
    {
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }
    public void GameEnd()
    {
        Application.Quit();
    }

    public void OptionBack()
    {
        MainMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }
    public void Continue()
    {
        SceneManage.Instance.UpdownFadeIn(true);

        Invoke("LoadContinueScene", 0.3f);
    }
    public void LoadContinueScene()
    {
        SceneManage.Instance.SaveSceneLoad();
    }

}
