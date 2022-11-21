using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePreScene : MonoBehaviour
{
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        portal = this.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject != null & collision.gameObject.tag == "Player")
            {
                SceneManage.Instance.PreSceneLoad();
            }
    }
}
