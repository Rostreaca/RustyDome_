using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
	
	[Header("저장될 자료형")]
	public float hp ;
	public int ammo ;
	public bool isGameLoaded = false;
	public static DataManager instance;

	public GameObject player;

	public PlayerController player_stats;

	public void Singleton_Init()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		instance = this;
	}
	private void Awake()
    {
		Singleton_Init();
    }
    void Update()
	{

		if (player ==null && GameObject.FindWithTag("Player") )
        {
			player = GameObject.FindWithTag("Player");
			player_stats = player.GetComponent<PlayerController>();

		}

		if (player_stats != null)
		{
			hp = player_stats.hpNow;
			ammo = player_stats.ammoNow;

			if(GameManager.Instance.isEnd == true)
            {
				SaveData character = new SaveData(hp, ammo);

				SaveSystem.Save(character);
			}
		}

		if (isGameLoaded ==true )
        {
			Invoke("LoadData", 0.1f);
        }

		DontDestroyOnLoad(this.gameObject);
	}
    private void LateUpdate()
    {
        
    }
    public void LoadData()
    {

		SaveData loadData = SaveSystem.Load();

		if(player_stats!=null)
        {
			player_stats.hpNow = loadData.hp;
			player_stats.ammoNow = loadData.bullets;
		}

		isGameLoaded = false;
	}
}
[System.Serializable]
public class SaveData
{
    public SaveData( float _hp, int _bullets)
    {
        hp = _hp;
        bullets = _bullets;
    }

    public float hp;
    public int bullets;

}
public static class SaveSystem
{
	private static string SavePath = Application.persistentDataPath + "/saves/";

	public static void Save(SaveData saveData)
	{
		if (!Directory.Exists(SavePath))
		{
			Directory.CreateDirectory(SavePath);
		}

		string saveJson = JsonUtility.ToJson(saveData);

		string saveFilePath = SavePath + "savedata.json";
		File.WriteAllText(saveFilePath, saveJson);
		Debug.Log("저장 성공: " + saveFilePath);
	}

	public static SaveData Load()
	{
		string saveFilePath = SavePath + "savedata.json";

		if (!File.Exists(saveFilePath))
		{
			Debug.LogError("저장된 파일이 없습니다.");
			return null;
		}

		string saveFile = File.ReadAllText(saveFilePath);
		SaveData saveData = JsonUtility.FromJson<SaveData>(saveFile);
		return saveData;
	}
}