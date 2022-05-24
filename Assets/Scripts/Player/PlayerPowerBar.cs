using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerBar : MonoBehaviour
{
    public Image powerbar;
    // Start is called before the first frame update
    void Start()
    {
        powerbar = GetComponent<Image>();
    }

    void PlayerMaxbar()
    {
        float curpower = PlayerController.instance.powerNow;
        float maxpower = PlayerController.instance.powerMax;
        powerbar.fillAmount = Mathf.Lerp(powerbar.fillAmount, curpower / maxpower, Time.deltaTime * 15f);
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMaxbar();
    }
}
