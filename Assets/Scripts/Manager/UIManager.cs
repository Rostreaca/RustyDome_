using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public PlayerController player;

    public enum ScreenState {Game, Pause, Inform, Map, Inventory, Customize, GameOver}
    public Image hpBar, powerBar, ammoBar;
    public Text scrapText;

    [Header("Components")]
    public CanvasGroup gameScreen, pauseScreen, mapScreen, inventoryScreen, customizeScreen, gameoverScreen;
    [HideInInspector]
    public ScreenState screenState;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        hpBar = GameObject.Find("Healthbar_Health").GetComponent<Image>();
        powerBar = GameObject.Find("Powerbar_Power").GetComponent<Image>();
        ammoBar = GameObject.Find("Ammobar_Ammo").GetComponent<Image>();
    }

    private void Update()
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

        if (Input.GetKeyDown(KeyCode.U) && WorkBenchController.instance.canopenCustomize == true)
        {
            if (customizeScreen.alpha > 0)
                ChangeScreen(ScreenState.Game);
            else
                ChangeScreen(ScreenState.Customize);
        }

        if (!GameManager.Instance.isPause)
            UpdateGameScreen();
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
                ScreenActive(mapScreen, true);
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
                //GameManager.Instance.isPause = true;
                //Time.timeScale = 0;
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

    public bool IsScreenOn(CanvasGroup canvasGroup)
    {
        return canvasGroup.blocksRaycasts;
    }

    public void UpdateGameScreen()
    {
        UpdateHealthBar();
        UpdatePowerBar();
        UpdateAmmoBar();

        UpdateScrap();
    }

    public void UpdateHealthBar()
    {
        float hp = PlayerController.instance.hpNow;
        float maxHp = PlayerController.instance.hpMax;
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, hp / maxHp, Time.deltaTime * 15f);
    }

    public void UpdatePowerBar()
    {
        float curPower = PlayerController.instance.powerNow;
        float maxPower = PlayerController.instance.powerMax;
        powerBar.fillAmount = Mathf.Lerp(powerBar.fillAmount, curPower / maxPower, Time.deltaTime * 15f);
    }

    public void UpdateAmmoBar()
    {
        float curAmmo = PlayerController.instance.ammoNow;
        float maxAmmo = PlayerController.instance.ammoMax;
        ammoBar.fillAmount = ((curAmmo / maxAmmo) * 10 * 0.09f) + 0.05f;
    }

    public void UpdateScrap()
    {
        scrapText.text = player.scrap.ToString();
    }
}
