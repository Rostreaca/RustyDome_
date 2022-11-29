using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionButton : MonoBehaviour
{
    public Button backbtn;
    // Start is called before the first frame update
    void Start()
    {
        backbtn.onClick.AddListener(_Back);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void _Back()
    {
        UIManager.instance.ActiveOption();
    }
}
