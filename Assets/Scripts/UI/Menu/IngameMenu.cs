using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IngameMenu : MonoBehaviour
{
    public static IngameMenu instance;
    public Button rebutton,optionbutton,quitbutton;
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
        constructor();
        rebutton.onClick.AddListener(_Restart);
        if(optionbutton !=null)
        {
            optionbutton.onClick.AddListener(_OptionMenu);
        }
        quitbutton.onClick.AddListener(_GameQuit);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void _Restart()
    {
        GameObject player;
        player = GameObject.FindWithTag("Player");
        GameManager.Instance.isPause = false;
        GameManager.Instance.isGame = true;
        Time.timeScale = 1;
        Destroy(player);

        DataManager data = GameObject.Find("DataManager").GetComponent<DataManager>();
        data.Restart();

    }
    public void _GameQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void _OptionMenu()
    {
        UIManager.instance.ActiveOption();
    }
}
