using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainCanvas : MonoBehaviour
{
    public static MaintainCanvas Instance;
    public void Singleton_Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Singleton_Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
