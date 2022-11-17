using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu, OptionMenu;
    public Button StartButton,OptionButton,EndButton,BackButton,ContinueButton;

    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("TheScene");
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
        SceneManager.LoadScene("TheScene");

        DataManager.instance.isGameLoaded = true;
    }

}
