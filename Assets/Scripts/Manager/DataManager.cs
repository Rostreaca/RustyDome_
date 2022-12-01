using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class DataManager : MonoBehaviour
{
	public int _loadSceneIndex;
	[Header("저장될 자료형")]
	public SaveData loadData;
	public float hp ;
	public int ammo ;
	public float power;
	public int scrap;
	public float player_pos_x, player_pos_y;
	public bool isquestClear;
	public bool isQuestStart;
	public int questprogress;
	public int SaveSceneIndex;
	public bool isbossDead;
	public bool HaveLever;
	public bool HaveGatekey;
	public bool isGateOpen,isDoorOpen;
	public bool isGameLoaded= false; 
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
		DontDestroyOnLoad(gameObject);
	}
	private void Awake()
	{
		//loadData = SaveSystem.Load("Main");
		//_loadSceneIndex = loadData.sceneIndex;
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
			//if(GameManager.Instance.checkPoint!=null)
   //         {
			//	player_pos_x = GameManager.Instance.checkPoint.position.x;
			//	player_pos_y = GameManager.Instance.checkPoint.position.y;
			//}
			player_pos_x = PlayerController.instance.transform.position.x;
			player_pos_y = PlayerController.instance.transform.position.y;
			isQuestStart = GameManager.Instance.isQuestStart;
			isquestClear = GameManager.Instance.isQuestClear;
			questprogress = QuestManager.instance.Enemycount;
			SaveSceneIndex = SceneManage.Instance.nowscene.buildIndex;
			isbossDead = GameManager.Instance.isBossDead;
			HaveLever = GameManager.Instance.HaveLever;
			HaveGatekey = GameManager.Instance.HaveGateKey;
			isGateOpen = GameManager.Instance.isGateOpen;
			isDoorOpen = GameManager.Instance.isDoorOpen;
			if (GameManager.Instance.isSave == true)
			{
				GameManager.Instance.isSave = false;

				SaveData character = new SaveData(player_pos_x,player_pos_y, hp, power, ammo, scrap, SaveSceneIndex,isQuestStart, questprogress,isquestClear,isbossDead,HaveLever,HaveGatekey,isGateOpen,isDoorOpen);

				SaveSystem.Save(character,"Main");

			}
		}
		loadData = SaveSystem.Load("Main");
		if (MenuManager.Instance !=null)
        {
			MenuManager.Instance.SceneIndex = loadData.sceneIndex;
		}

		if (isGameLoaded == true)
		{

			Invoke("LoadData", 0.1f);
		}

		_loadSceneIndex = loadData.sceneIndex;
	}
	public void Restart()
    {
		if (_loadSceneIndex != 0)
		{
			SceneManager.LoadScene(_loadSceneIndex);
		}
		else if(_loadSceneIndex == 0)
        {
			Debug.Log("로드인덱스가 0입니다.");
        }

		isGameLoaded = true;
    }
    public void LoadData()
    {
		if (player_stats!=null)
        {
			player_stats.transform.position = new Vector2(loadData.player_pos_x,loadData.player_pos_y);
			player_stats.hpNow = loadData.hp;
			player_stats.ammoNow = loadData.bullets;
			player_stats.powerNow = loadData.powers;
			player_stats.scrap = loadData.scraps;
		}
		if(GameManager.Instance !=null)
		{
			GameManager.Instance.isQuestStart = loadData.isQuestStart;
			GameManager.Instance.isQuestClear = loadData.isquestClear;
			GameManager.Instance.isDoorOpen = loadData.isDoorOpen;
			GameManager.Instance.isBossDead = loadData.isbossDead;
			GameManager.Instance.HaveLever = loadData.HaveLever;
			GameManager.Instance.HaveGateKey = loadData.HaveGatekey;
			GameManager.Instance.isGateOpen = loadData.isGateOpen;
		}
		if(QuestManager.instance !=null)
        {
			QuestManager.instance.Enemycount = questprogress;
		}

		isGameLoaded = false;
	}
}

[System.Serializable]
public class SaveData
{
    public SaveData(float _player_pos_x,float _player_pos_y, float _hp, float _powers, int _bullets, int _scraps, int _sceneIndex,
		bool _isQuestStart,int _questprogress,bool _isquestClear ,bool _isbossDead, bool _HaveLever, bool _HaveGatekey, bool _isGateOpen, bool _isDoorOpen)
    {
		player_pos_x = _player_pos_x;
		player_pos_y = _player_pos_y;
        hp = _hp;
		powers = _powers;
        bullets = _bullets;
		scraps = _scraps;
		sceneIndex = _sceneIndex;
		isQuestStart = _isQuestStart;
		questprogress = _questprogress;
		isquestClear = _isquestClear;
		isbossDead = _isbossDead;
		HaveLever = _HaveLever;
		HaveGatekey = _HaveGatekey;
		isGateOpen = _isGateOpen;
		isDoorOpen = _isDoorOpen;

	}
	public float player_pos_x, player_pos_y;
    public float hp;
	public float powers;
    public int bullets;
	public int scraps;
	public int sceneIndex;
	public int questprogress;
	public bool isQuestStart;
	public bool isquestClear;
	public bool isbossDead;
	public bool HaveLever;
	public bool HaveGatekey;
	public bool isGateOpen;
	public bool isDoorOpen;

}
public static class SaveSystem
{
	private static string SavePath = Application.persistentDataPath + "/saves/";

	public static void Save(SaveData saveData, string filename)
	{
		if (!Directory.Exists(SavePath))
		{
			Directory.CreateDirectory(SavePath);
		}

		string saveJson = JsonUtility.ToJson(saveData);

		string saveFilePath = SavePath + filename +"savedata.json";
		File.WriteAllText(saveFilePath, saveJson);
		Debug.Log("저장 성공: " + saveFilePath);
	}

	public static SaveData Load(string filename)
	{
		string saveFilePath = SavePath + filename +"savedata.json";

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