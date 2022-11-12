using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SoundSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider bgmsoundcon,effectsoundcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(bgmsoundcon == null && GameObject.Find("BgmSlider"))
        {
            bgmsoundcon = GameObject.Find("BgmSlider").GetComponent<Slider>();
        }

        if(effectsoundcon == null && GameObject.Find("SfxSlider"))
        {
            effectsoundcon = GameObject.Find("SfxSlider").GetComponent<Slider>();
        }
        mixer.SetFloat("Sfx", effectsoundcon.value);
        mixer.SetFloat("Bgm", bgmsoundcon.value);
        //SoundManager.instance.volume = effectsoundcon.value;
    }
}
