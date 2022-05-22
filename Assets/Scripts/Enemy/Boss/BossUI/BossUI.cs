using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Image hpbar;
    public Image Backhpbar;

    
    public bool backhit;
    // Start is called before the first frame update
    void Start()
    {
        hpbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        BossHPBar();
        BossBackHPBar();
    }
    
    void BossHPBar()
    {
        float bosshp = BossController.instance.hpNow;
        float bossmaxhp = BossController.instance.hpMax;
        hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, bosshp / bossmaxhp,Time.deltaTime * 15f);
    }
    void BossBackHPBar()
    {
        float bosshp = BossController.instance.hpNow;
        float bossmaxhp = BossController.instance.hpMax;
        Backhpbar.fillAmount = Mathf.Lerp(Backhpbar.fillAmount, bosshp / bossmaxhp, Time.deltaTime * 1f);
    }

}
