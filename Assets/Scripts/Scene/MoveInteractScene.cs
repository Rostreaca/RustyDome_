using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveInteractScene : MonoBehaviour
{
    [Header("이동할 씬 넘버")]
    public int PassSceneNumber;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject !=null && collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.F))
            {
                SceneManage.Instance.UpdownFadeIn(false);

                Invoke("LoadScene", 0.3f);
            }
        }
    }

    public void LoadScene()
    {
        SceneManage.Instance.InteractSceneLoad(PassSceneNumber);
    }
}
