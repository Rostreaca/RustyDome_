using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    void SingletonInit()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public bool NowLoading;
    public bool BossCutscenePlaying;
    public bool RuinCutscenePlaying;
    [Header("Parameters")]
    public bool isGame;
    public bool isPause;
    public bool isSave;
    public bool isEnd;
    [Header("PlayingDatas")]
    public bool[] boxopened;
    public bool Scene2MissonStart;
    public bool Scene2MissonClear;
    public bool isQuestStart;
    public bool isQuestClear;
    public bool RepeatTalk;
    public bool isDoorOpen;
    public bool isBossDead;
    public bool isGateOpen;
    public bool HaveLever;
    public bool HaveGateKey;
    public int coinCount;
    [Header("Checkpoint")]
    public Transform checkPoint;
    private void Awake()
    {
        SingletonInit();
    }

    private void Start()
    {
        StartGame(); //when scene load game'll start
    }

    private void Update()
    {
    }

    public void StartGame()
    {
        isGame = true;
    }

    public void GameOver()
    {
        isGame = false; //disable game
        UIManager.instance.ChangeScreen(UIManager.ScreenState.GameOver); //change screen to lose
    }
    

}
