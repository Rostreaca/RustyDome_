using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform playerPosition;
    public GameObject dialog;
    
    
    private static float seeRange = 0.5f;
    private bool isSee;
    private bool isSay;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Check();
        CreateTextBox();
    }

    void Check()
    {

        if (Vector2.Distance(transform.position, playerPosition.position) < seeRange)
        {
            isSee = true;
        }
        else
            isSee = false;
    }

    void CreateTextBox()
    {
        
        if (isSee == true)
        {
            if (isSay == false)
            {
                dialog.SetActive(true);
            }
            isSay = true;
        }
        else if(isSee != true)
        {
            dialog.SetActive(false);
            isSay = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, seeRange);
    }

}
