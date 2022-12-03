using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraManager : MonoBehaviour
{
    public Canvas canvas;
    public static CameraManager instance; //static camera for use in other scripts

    Transform player,boss, NPC1 , LadderPos; //Player position

    public Vector2 offset; //Camera offset by player position
    public float cameraXPosMin, cameraXPosMax;
    public float cameraYPosMin, cameraYPosMax; //Camera position clamp
    public float moveSpeed;

    public BoxCollider2D bound;
    private Vector3 minBound, maxBound;

    private float halfWidth, halfHeight;

    private Camera theCamera;

    void SingletonInit()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private void Awake()
    {
        SingletonInit();
    }

    private void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }
    public bool CameraLanding;
    public void FixedUpdate()
    {
        if(GameObject.Find("Canvas")!=null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            canvas.worldCamera = gameObject.GetComponent<Camera>();
        }
        if (GameObject.Find("SceneManager"))
        {
            Canvas scenemanager = GameObject.Find("SceneManager").GetComponent<Canvas>();

            scenemanager.worldCamera = gameObject.GetComponent<Camera>();
        }

        player = GameObject.FindGameObjectWithTag("Player").transform; //Find player in scene

        if (GameObject.Find("Anime_Boss"))
        {
            boss = GameObject.Find("Anime_Boss").transform;
        }
        if(GameObject.Find("NPC"))
        {
            NPC1 = GameObject.Find("NPC").transform;
        }
        if(GameObject.Find("LadderCutScene"))
        {
            LadderPos = GameObject.Find("LadderCutScene").transform;
        }
        if (GameManager.Instance.BossCutscenePlaying == true)
        {
            Vector3 BossPos = new Vector3(boss.position.x + offset.x, boss.position.y + offset.y + 1.5f, transform.position.z); 
            transform.position = Vector3.Lerp(transform.position, BossPos, moveSpeed/2 * Time.deltaTime);
        }
        else if(GameManager.Instance.RuinCutscenePlaying == true)
        {
            FollowRuinCutscene();
        }
        else
        {
            Vector3 newPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y + 1.5f, transform.position.z); //Local vector get player position
            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed * Time.deltaTime ); //Set camera position smooth
        }
        

        float clampedX = Mathf.Clamp(transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z); //make clamp
    }
    public void FollowRuinCutscene()
    {
        if (RuinCutScene.instance.TalkEnd == false)
        {
            Vector3 NPCPos = new Vector3(NPC1.position.x + offset.x, NPC1.position.y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, NPCPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 Ladder = new Vector3(LadderPos.position.x + offset.x, LadderPos.position.y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, Ladder, moveSpeed * Time.deltaTime);
        }
    }
    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}