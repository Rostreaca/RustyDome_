using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixerGroup sfxgroup;
    public AudioSource sfx;

    public float volume;
    public void singleton_Init()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance !=null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Awake()
    {
        singleton_Init();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SFXPlay(string sfxName, AudioClip clip) //ȿ���� ���, ����� �κп��� script �� �Լ� �߰�.
    {
            GameObject go = new GameObject(sfxName + "Sound");
            AudioSource audiosource = go.AddComponent<AudioSource>();
            sfx = audiosource;
        sfx.outputAudioMixerGroup = sfxgroup;
            sfx.clip = clip;
            sfx.volume = volume;
            sfx.Play();
        

            Destroy(go, clip.length);
    }



}
