using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IngameMenu : MonoBehaviour
{
    public GameObject IngameCam, Managers, Canvas;
    GameObject player;
    public static IngameMenu instance;
    public Button continuebutton,rebutton,optionbutton,quitbutton;
    // Start is called before the first frame update
    void constructor()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        if(SceneManage.Instance.nowscene.buildIndex !=0 && GameObject.Find("Canvas"))
        {
            Canvas = GameObject.Find("Canvas");
        }
        Managers = GameObject.Find("Managers");
           IngameCam = GameObject.Find("Main Camera");
           player = GameObject.FindWithTag("Player");
        constructor();
        if(continuebutton != null) { continuebutton.onClick.AddListener(_Continue); }
        if (rebutton != null) { rebutton.onClick.AddListener(_Restart); };
        if(optionbutton !=null){ optionbutton.onClick.AddListener(_OptionMenu); }
        if (quitbutton != null) { quitbutton.onClick.AddListener(_GameQuit); };
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void _Continue()
    {
        UIManager.instance.ChangeScreen(UIManager.ScreenState.Game);
    }
    public void _Restart()
    {
        GameManager.Instance.isPause = false;
        GameManager.Instance.isGame = true;
        UIManager.instance.gameoverScreen.alpha = 0;
        UIManager.instance.gameoverScreen.blocksRaycasts = false;
        Time.timeScale = 1;
        Destroy(player);
        Destroy(Canvas);
        Destroy(Managers);
        DataManager data = GameObject.Find("DataManager").GetComponent<DataManager>();
        data.Restart();

    }

    public void _GameQuit()
    {
        Destroy(Managers);
        Destroy(player);
        Destroy(IngameCam);
        Destroy(Canvas);
        UIManager.instance.ChangeScreen(UIManager.ScreenState.Game);
        GameManager.Instance.isPause = false;

        //SceneManage.Instance.UpdownFadeIn(false);
        SceneManager.LoadScene(0);
        //Debug.Log("게임 종료");
        //Application.Quit();
    }

    public void Menu()
    {
    }
    public void _OptionMenu()
    {
        UIManager.instance.ActiveOption();
    }
}
