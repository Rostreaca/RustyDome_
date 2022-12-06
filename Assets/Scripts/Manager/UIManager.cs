using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public PlayerController player;

    public enum ScreenState {Game, Pause, Inform, Map, Inventory, Customize, GameOver, Option, End}
    public Image hpBar, powerBar, ammoBar;
    public Text scrapText;

    [Header("Components")]
    public CanvasGroup gameScreen, pauseScreen, mapScreen, inventoryScreen, customizeScreen, gameoverScreen, optionScreen ,endScreen;
    [HideInInspector]
    public ScreenState screenState;

    bool finished;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void FindCanvasGroup()
    {
            if (GameObject.Find("GameScreen"))
            {
                gameScreen = GameObject.Find("GameScreen").GetComponent<CanvasGroup>();

                hpBar = GameObject.Find("Healthbar_Health").GetComponent<Image>();
                powerBar = GameObject.Find("Powerbar_Power").GetComponent<Image>();
                ammoBar = GameObject.Find("Ammobar_Ammo").GetComponent<Image>();

                scrapText = GameObject.Find("ScrapText").GetComponent<Text>();
            }
        if (GameObject.Find("PauseScreen"))
            pauseScreen = GameObject.Find("PauseScreen").GetComponent<CanvasGroup>();
        if (GameObject.Find("MapScreen"))
            mapScreen = GameObject.Find("MapScreen").GetComponent<CanvasGroup>();
        if (GameObject.Find("InventoryScreen"))
            inventoryScreen = GameObject.Find("InventoryScreen").GetComponent<CanvasGroup>();
        if (GameObject.Find("CustomizeScreen"))
            customizeScreen = GameObject.Find("CustomizeScreen").GetComponent<CanvasGroup>();
        if (GameObject.Find("GameOverScreen"))
            gameoverScreen = GameObject.Find("GameOverScreen").GetComponent<CanvasGroup>();
        if (GameObject.Find("OptionScreen"))
            optionScreen = GameObject.Find("OptionScreen").GetComponent<CanvasGroup>();
        if(GameObject.Find("FinishScreen"))
            endScreen = GameObject.Find("FinishScreen").GetComponent<CanvasGroup>();
        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (GameObject.Find("Healthbar_Health"))
            hpBar = GameObject.Find("Healthbar_Health").GetComponent<Image>();
        if (GameObject.Find("Powerbar_Power"))
            powerBar = GameObject.Find("Powerbar_Power").GetComponent<Image>();
        if (GameObject.Find("Ammobar_Ammo"))
            ammoBar = GameObject.Find("Ammobar_Ammo").GetComponent<Image>();
    }
    private void Start()
    {
        FindCanvasGroup();
    }

    private void Update()
    {
        FindCanvasGroup();
        if (Input.GetKeyDown(KeyCode.Escape) && !finished)
        {
            if (GameManager.Instance.isPause )
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

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (customizeScreen.alpha > 0)
            {
                ChangeScreen(ScreenState.Game);
                Customize.instance.canCustomize = false;
            }

            else
                ChangeScreen(ScreenState.Customize);
        }
        if (!GameManager.Instance.isPause)
            UpdateGameScreen();
    }
    public void ActiveOption()
    {
        if (optionScreen.alpha > 0)
            ChangeScreen(ScreenState.Pause);
        else
            ChangeScreen(ScreenState.Option);
    }
    public void ChangeScreen(ScreenState screenState)
    {
        //일단 다끄고 해당되는걸 킨다.
        ScreenActive(gameScreen, false);
        ScreenActive(pauseScreen, false);
        ScreenActive(mapScreen, false);
        ScreenActive(inventoryScreen, false);
        ScreenActive(customizeScreen, false);
        ScreenActive(optionScreen, false);
        ScreenActive(endScreen, false);

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

            case ScreenState.Option:
                ScreenActive(optionScreen, true);
                GameManager.Instance.isPause = true;
                Time.timeScale = 0;
                break;
            case ScreenState.End:
                ScreenActive(endScreen, true);
                finished = true;
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

    public bool IsScreenOn(CanvasGroup canvasGroup)
    {
        return canvasGroup.alpha == 1;
    }

    public void UpdateGameScreen()
    {
        if(player !=null)
        {
            UpdateHealthBar();
            UpdatePowerBar();
            UpdateAmmoBar();

            UpdateScrap();
        }
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
