using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveNextScene : MonoBehaviour
{
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        portal = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject != null & collision.gameObject.tag == "Player")
        {
            if (PlayerController.instance.transform.position.x < portal.transform.position.x)
            {
                SceneManage.Instance.FadeIn(false);
            }
            else if(PlayerController.instance.transform.position.x > portal.transform.position.x)
            {
                SceneManage.Instance.FadeIn(true);
            }
            Invoke("SceneLoad", 0.3f);
            }
    }

    public void SceneLoad()
    {
        
        SceneManage.Instance.NextSceneLoad();
    }
}
