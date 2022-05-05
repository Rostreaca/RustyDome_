using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void SingletonInit()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    [Header("Parameters")]
    public bool isGame;
    public bool isPause;

    private void Awake()
    {
        SingletonInit();
    }

    private void Start()
    {
        StartGame(); //when scene load game'll start
    }

    public void StartGame()
    {
        isGame = true;
    }
}
