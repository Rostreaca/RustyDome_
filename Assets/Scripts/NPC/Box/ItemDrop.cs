using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    public ItemMagnetic iteminfo;
    private void Start()
    {
        if (ItemMagnetic.instance != null)
        {
            iteminfo = ItemMagnetic.instance;
        }
    }
    private void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.CompareTag("Player")&& iteminfo.isGrounded == true)
                {
                    Destroy(gameObject);
                    GameManager.Instance.coinCount++; //아이템 획득시 이벤트(현재는 코인 개수 증가)
                }
    }
    
}
