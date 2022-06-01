using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public GameObject dialog;
    public Transform playerPos;


    public bool hasdialog;
    public bool isSee;
    public bool isSay;

    public float seeRange = 0.5f;
    private void Awake()
    {
        playerPos = PlayerController.instance.transform;
    }

    // Start is called before the first frame update
    public void Check()
    {

        if (Vector2.Distance(transform.position, playerPos.position) < seeRange)
        {
            isSee = true;
        }
        else
            isSee = false;
    }

    public void CreateTextBox()
    {

        if (isSee == true)
        {
            if (isSay == false)
            {
                dialog.SetActive(true);
            }
            isSay = true;
        }
        else if (isSee != true)
        {
            dialog.SetActive(false);
            isSay = false;
        }
    }


}
