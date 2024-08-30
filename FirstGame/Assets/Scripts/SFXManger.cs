using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManger : MonoBehaviour
{
    public static SFXManger instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] soundEffects;

    public void PlaySFX(int sfxToPlay)
    {   //当相同的音效重叠播放时，停止前一个音效
        soundEffects[sfxToPlay].Stop();//停止前一个音效
        soundEffects[sfxToPlay].Play();//播放当前音效
    }

    public void PlaySFXPitch(int sfxToPlay)
    {
        soundEffects[sfxToPlay].pitch = Random.Range(0.8f, 1.2f);//音高随机

        PlaySFX(sfxToPlay);
    }
}
