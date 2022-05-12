using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.Instance.coinCount++; //아이템 획득시 이벤트(현재는 코인 개수 증가)
        }
    }
}
