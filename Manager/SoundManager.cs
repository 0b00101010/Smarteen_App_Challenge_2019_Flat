using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    public float BGMVoule{
        get => bgmVolume;
        set {
            bgmVolume = value;
            PlayerPrefs.SetFloat("BGM",value);
        }
    }

    public float SFXVolume{
        get => sfxVolume;
        set {
            sfxVolume = value;
            PlayerPrefs.SetFloat("SFX",value);
        }
    }

    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioSource sfxSource;
    private void Start(){
        if(PlayerPrefs.HasKey("BGM"))
            bgmVolume = PlayerPrefs.GetFloat("BGM");
        else
            PlayerPrefs.SetFloat("BGM",0.5f);
            
        if(PlayerPrefs.HasKey("SFX"))
            sfxVolume = PlayerPrefs.GetFloat("SFX");
        else
            PlayerPrefs.SetFloat("SFX",0.5f);
            
        
    }

    public void ChangeBGM(AudioClip bgmClip){
        bgmSource.clip = bgmClip;
    }

    public void PlayBgm(){
        bgmSource.Play();
    }

    public void StopBGM(){
        bgmSource.Stop();
    }

    public void SFXOneShot(AudioClip sfxClip){
        sfxSource.PlayOneShot(sfxClip);
    }
}
