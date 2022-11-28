using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SoundSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider soundcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(soundcon == null && GameObject.Find("SoundSlider"))
        {
            soundcon = GameObject.Find("SoundSlider").GetComponent<Slider>();
        }
        if(soundcon != null)
        {
            mixer.SetFloat("Master", soundcon.value);
        }
        //SoundManager.instance.volume = effectsoundcon.value;
    }
}
