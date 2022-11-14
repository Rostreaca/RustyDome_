using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerController SaveData ;
    public GameManager SaveGameManager;
    public void Singleton_Init()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    // Start is called before the first frame update

    private void Awake()
    {
        Singleton_Init();

    }
    [ContextMenu("To Json Data")]
    void JsonSaveData()
    {
        string jsondata = JsonUtility.ToJson(SaveData);
        string path = Application.dataPath + "/SaveData.json";
        File.WriteAllText(path, jsondata);
    }
    [ContextMenu("Load Json Data")]
    void JsonLoadData()
    {
        string path = Application.dataPath + "/SaveData.json";
        if(File.Exists(path))
        {
            string FromJsonData = File.ReadAllText(path);
            SaveData = JsonUtility.FromJson<PlayerController>(FromJsonData);
        }
        else
        {
            SaveData = new PlayerController();
        }
    }
    
}
