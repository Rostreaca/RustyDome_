using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour
{
    public ItemMagnetic iteminfo;
    // Start is called before the first frame update
    // Update is called once per frame

    private void Update()
    {
        if (PlayerController.instance.ammoNow != PlayerController.instance.ammoMax)
        {
            iteminfo.CanFollow = true;
        }
        else
            iteminfo.CanFollow = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iteminfo.isGrounded == true)
        {
            if (PlayerController.instance.ammoNow != PlayerController.instance.ammoMax)
            {
                PlayerController.instance.ammoNow++;
                Destroy(gameObject);
            }
        }
    }
}
