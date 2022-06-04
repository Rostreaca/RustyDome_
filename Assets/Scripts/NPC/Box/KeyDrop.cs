using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    public ItemMagnetic iteminfo;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iteminfo.isGrounded == true)
        {
            GameManager.Instance.hasGateKey = true;
            Destroy(gameObject);
        }
    }
}
