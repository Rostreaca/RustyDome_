using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public enum ScreenState { Game, Pause, Inform, Map, Inventory, Customize}

    [Header("Components")]
    public GameObject gameScreen, pauseScreen, mapScreen, inventoryScreen, customizeScreen;
    [HideInInspector]
    public ScreenState screenState;

    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Awake()
    {
        SingletonInit();
    }

    public void ChangeScreen(ScreenState screenState)
    {
        gameScreen.SetActive(false);
        pauseScreen.SetActive(false);
        mapScreen.SetActive(false);
        inventoryScreen.SetActive(false);
        customizeScreen.SetActive(false);

        switch (screenState)
        {
            case ScreenState.Game: //if game
                                   //turn off other and turn on game
                gameScreen.SetActive(true);

                //Disable pause
                GameManager.Instance.isPause = false;
                //return normal time
                Time.timeScale = 1;
                break;

            case ScreenState.Pause:
                pauseScreen.SetActive(true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;

            case ScreenState.Map:
                mapScreen.SetActive(true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;

            case ScreenState.Inventory:
                inventoryScreen.SetActive(true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;

            case ScreenState.Customize:
                customizeScreen.SetActive(true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;
        }
    }

    public void Pause()
    {
        ChangeScreen(ScreenState.Pause);
    }

    public void UnPause()
    {
        ChangeScreen(ScreenState.Game);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isPause)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Pause);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapScreen.activeInHierarchy)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Map);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryScreen.activeInHierarchy)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Inventory);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (customizeScreen.activeInHierarchy)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Customize);
        }

    }
}
