using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResolutionManager : MonoBehaviour
{
    public bool isFull = true;
    public int res;
    public static ResolutionManager instance;
    // Start is called before the first frame update

    void Singletone_Init()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }    
    void Start()
    {
        Singletone_Init();
    }

    // Update is called once per frame
    void Update()
    {
        if ( res ==0)
        {
            Screen.SetResolution(1920, 1080, isFull);
        }
        if(res == 1)
        {
            Screen.SetResolution(1280, 720, isFull);
        }
        if (res == 2)
        {
            Screen.SetResolution(960, 540, isFull);
        }
    }
}
