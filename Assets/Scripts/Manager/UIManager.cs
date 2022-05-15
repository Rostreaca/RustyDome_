using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public enum ScreenState { Game, Pause, Inform, Map, Inventory, Customize, GameOver}

    [Header("Components")]
    public CanvasGroup gameScreen, pauseScreen, mapScreen, inventoryScreen, customizeScreen, gameoverScreen;
    [HideInInspector]
    public ScreenState screenState;

    void SingletonInit()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Awake()
    {
        SingletonInit();
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
            if (mapScreen.alpha > 0)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Map);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryScreen.alpha > 0)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Inventory);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (customizeScreen.alpha > 0)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Customize);
        }

    }

    public void ChangeScreen(ScreenState screenState)
    {
        //일단 다끄고 해당되는걸 킨다.
        ScreenActive(gameScreen, false);
        ScreenActive(pauseScreen, false);
        ScreenActive(mapScreen, false);
        ScreenActive(inventoryScreen, false);
        ScreenActive(customizeScreen, false);

        switch (screenState)
        {
            case ScreenState.Game: //if game
                                   //turn off other and turn on game
                ScreenActive(gameScreen, true);

                //Disable pause
                GameManager.Instance.isPause = false;
                //return normal time
                Time.timeScale = 1;
                break;

            case ScreenState.Pause:
                ScreenActive(pauseScreen, true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;

            case ScreenState.Map:
                ScreenActive(mapScreen, false);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;

            case ScreenState.Inventory:
                ScreenActive(inventoryScreen, true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;

            case ScreenState.Customize:
                ScreenActive(customizeScreen, true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;

            case ScreenState.GameOver:
                ScreenActive(gameoverScreen, true);
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

    public void ScreenActive(CanvasGroup canvasGroup, bool value)
    {
        if (value)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
