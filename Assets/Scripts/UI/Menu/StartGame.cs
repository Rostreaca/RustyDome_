using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button btn1;
    // Start is called before the first frame update
    void Start()
    {
        btn1.onClick.AddListener(GameStart);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        SceneManager.LoadScene("TheScene");
    }
}
