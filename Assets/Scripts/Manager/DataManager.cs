using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{

	[Header("저장될 자료형")]
	public Transform player_pos;
	public float hp ;
	public int ammo ;
	public float power;
	public int scrap;
	//public int questprogress;
	public bool isbossDead;
	public bool HaveLever;
	public bool HaveGatekey;
	public bool isGateOpen;
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
			//PlayerController에서 가져올 값들
			hp = player_stats.hpNow;
			ammo = player_stats.ammoNow;
			power = player_stats.powerNow;
			scrap = player_stats.scrap;
			//GameManager에서 가져올 값들
			isbossDead = GameManager.Instance.isBossDead;
			player_pos = GameManager.Instance.checkPoint;
			HaveLever = GameManager.Instance.HaveLever;
			HaveGatekey = GameManager.Instance.HaveGateKey;
			isGateOpen = GameManager.Instance.isGateOpen;
			if (GameManager.Instance.isSave == true)
            {
				SaveData character = new SaveData(player_pos, hp, power, ammo, scrap, isbossDead,HaveLever,HaveGatekey,isGateOpen);

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
			player_stats.transform.position = loadData.player_pos.position;
			player_stats.hpNow = loadData.hp;
			player_stats.ammoNow = loadData.bullets;
			player_stats.powerNow = loadData.powers;
			player_stats.scrap = loadData.scraps;
		}

		GameManager.Instance.isBossDead = loadData.isbossDead;
		GameManager.Instance.HaveLever = loadData.HaveLever;
		GameManager.Instance.HaveGateKey = loadData.HaveGatekey;
		GameManager.Instance.isGateOpen = loadData.isGateOpen;

		isGameLoaded = false;
	}
}
[System.Serializable]
public class SaveData
{
    public SaveData(Transform _player_pos, float _hp, float _powers, int _bullets, int _scraps,
		bool _isbossDead, bool _HaveLever, bool _HaveGatekey, bool _isGateOpen)
    {
		_player_pos = player_pos;
        hp = _hp;
		powers = _powers;
        bullets = _bullets;
		scraps = _scraps;
		isbossDead = _isbossDead;
		HaveLever = _HaveLever;
		HaveGatekey = _HaveGatekey;
		isGateOpen = _isGateOpen;
    }
	public Transform player_pos;
    public float hp;
	public float powers;
    public int bullets;
	public int scraps;
	//public int questprogress;
	public bool isbossDead;
	public bool HaveLever;
	public bool HaveGatekey;
	public bool isGateOpen;

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