using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverEvent : MonoBehaviour
{
    DoorController doorcontrol;
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DoorOpen()
    {
        GameManager.Instance.isDoorOpen = true;
    }

    public void SoundPlay(AudioClip audio)
    {
        SoundManager.instance.SFXPlay("aa", audio);
    }
}
