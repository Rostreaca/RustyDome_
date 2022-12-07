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

    public void OnResolutionChange()
    {
        ResolutionManager.instance.isFull = fullScr.isOn;

        if (drop.value == 0)
        {
            ResolutionManager.instance.SetResolution(0);
        }

        if (drop.value == 1)
        {
            ResolutionManager.instance.SetResolution(1);
        }

        if (drop.value == 2)
        {
            ResolutionManager.instance.SetResolution(2);
        }
    }
}
