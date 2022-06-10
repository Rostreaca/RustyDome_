using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public ItemMagnetic iteminfo;
    public Item item;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iteminfo.isGrounded == true)
        {
            Inventory.instance.AddItem(item);
            GameManager.Instance.hasGateKey = true;
            Destroy(gameObject);
        }
    }
}
