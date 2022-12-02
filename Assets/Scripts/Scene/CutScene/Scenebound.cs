using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenebound : MonoBehaviour
{
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
        if(collision.gameObject!=null&& collision.gameObject.tag == "Enemy")
        {
            EnemyController _enemycon = collision.gameObject.GetComponent<EnemyController>();
            if(!GameManager.Instance.Scene2MissonStart)
            {
                _enemycon.inSceneBound = true;
            }
            else
            {
                _enemycon.inSceneBound = false;
            }
        }
    }
}
