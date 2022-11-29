using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResolutionSettings : MonoBehaviour
{
    public Toggle fullScr;
    Dropdown drop;
    // Start is called before the first frame update
    void Start()
    {
        drop = GetComponent<Dropdown>();
    }   
    

    // Update is called once per frame
    void Update()
    {
        ResolutionManager.instance.isFull = fullScr.isOn;
        if(drop.value == 0)
        {
            ResolutionManager.instance.res = 0;
        }

        if (drop.value == 1)
        {
            ResolutionManager.instance.res = 1;
        }
        if (drop.value == 2)
        {
            ResolutionManager.instance.res = 2;
        }
    }
}
