using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }
    public void Disabler()
    {
        gameObject.SetActive(false);
    }
}
