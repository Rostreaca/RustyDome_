using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandleDrop : MonoBehaviour
{
    public ItemMagnetic iteminfo;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iteminfo.isGrounded == true)
        {
            GameManager.Instance.hasLeverhandle = true;
            Destroy(gameObject);
        }
    }
}
