using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
        Singleton_Init();
        StartButton.onClick.AddListener(GameStart);
        OptionButton.onClick.AddListener(Option);
        EndButton.onClick.AddListener(GameEnd);
        BackButton.onClick.AddListener(OptionBack);
        ContinueButton.onClick.AddListener(Continue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        Scene Menuscene;
        Menuscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Menuscene.buildIndex+1);
        //SceneManager.LoadScene(8);
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
        SceneManager.LoadScene(SceneIndex);

        DataManager.instance.isGameLoaded = true;
    }

}
