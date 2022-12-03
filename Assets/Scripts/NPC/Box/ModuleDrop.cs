using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleDrop : MonoBehaviour
{
    public ItemMagnetic iteminfo;
    public Module module;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && iteminfo.isGrounded == true)
        {
            Customize.instance.AddModule(module);
            Destroy(gameObject);
        }
    }
}
