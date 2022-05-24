using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    public Image hpbar;
    // Start is called before the first frame update
    void Start()
    {
        hpbar = GetComponent<Image>();
    }

    void Plyaerhpbar()
    {
        float hp = PlayerController.instance.hpNow;
        float maxhp = PlayerController.instance.hpMax;
        hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, hp / maxhp, Time.deltaTime * 15f);
    }
    // Update is called once per frame
    void Update()
    {
        Plyaerhpbar();
    }
}
