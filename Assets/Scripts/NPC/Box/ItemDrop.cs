using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ItemMagnetic iteminfo;
    public int scrapValue = 5;

    public void anyevent()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iteminfo.isGrounded == true)
        {
            anyevent();
            collision.GetComponent<PlayerController>().GetScrap(scrapValue);
            Destroy(gameObject);
        }
    }  
}
