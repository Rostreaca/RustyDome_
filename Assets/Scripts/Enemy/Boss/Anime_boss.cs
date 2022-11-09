using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anime_boss : MonoBehaviour
{
    public GameObject Boss;
    public bool playerentered = false;
    public static Anime_boss instance;

    private Animator anim;
    private void SIngleton_Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Awake()
    {
        SIngleton_Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("PlayerEntering", playerentered);

    }

    public void EndMotion()
    {
        GameManager.Instance.CutscenePlaying = false;

        Instantiate(Boss, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        Destroy(gameObject);
    }
}
