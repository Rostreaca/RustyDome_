using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip bgmlist;
    public static SoundManager instance;
    public float volume;

    public void singleton_Init()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Awake()
    {
        BackgroundMusic(bgmlist);
        singleton_Init();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void SFXPlay(string sfxName, AudioClip clip) //효과음 재생, 재생할 부분에서 script 에 함수 추가.
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = volume;
        audiosource.Play();

        Destroy(go, clip.length);
    }

    public void BackgroundMusic(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.loop = true;
        bgm.Play();
    }
}
