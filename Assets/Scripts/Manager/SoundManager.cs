using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixerGroup sfxgroup;
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioClip[] bgmlist;

    public float volume;
    public int soundtracknumber;
    public bool isStop;
    public bool PlayingMusic = false;
    public void singleton_Init()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }
    private void Awake()
    {
        singleton_Init();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T)&&PlayingMusic ==false)
        {
            BackgroundMusic(bgmlist[soundtracknumber]);
            PlayingMusic = true;
        }
        if(Input.GetKey(KeyCode.Y) && PlayingMusic == true)
        {
            bgm.Stop();
            PlayingMusic = false;
        }
        if (Input.GetMouseButtonDown(2) )
        {
            if (soundtracknumber == bgmlist.Length-1)
            {
                soundtracknumber = 0;
            }
            else
            {
                soundtracknumber++;
            }
            BackgroundMusic(bgmlist[soundtracknumber]);
        }
        //if (soundtracknumber == bgmlist.Length)
        //{
        //    soundtracknumber = 0;
        //    BackgroundMusic(bgmlist[soundtracknumber]);
        //}

    }

    public void SFXPlay(string sfxName, AudioClip clip) //효과음 재생, 재생할 부분에서 script 에 함수 추가.
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


    public void BackgroundMusic(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.loop = true;
        bgm.Play();
    }

}
