using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private bool bgmOnOff;

    public bool BGMOnOff{
        get => bgmOnOff;
        set {
            bgmOnOff = value;
            PlayerPrefs.SetString("BGM",bgmOnOff.ToString());
            Setting();
        }
    }

    private bool sfxOnOff;

    public bool SFXOnOff{
        get => sfxOnOff;
        set {
            sfxOnOff = value;
            PlayerPrefs.SetString("SFX",sfxOnOff.ToString());
            Setting();
        }
    }

    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioSource sfxSource;
    private void Start(){

        if(PlayerPrefs.HasKey("BGM"))
            BGMOnOff = bool.Parse(PlayerPrefs.GetString("BGM"));
        else 
            BGMOnOff = true;

        if(PlayerPrefs.HasKey("SFX"))
            SFXOnOff = bool.Parse(PlayerPrefs.GetString("SFX"));

        else
            SFXOnOff = true;

    }

    private void Setting(){
        
        if(bool.Parse(PlayerPrefs.GetString("BGM")))
            bgmSource.volume = 1.0f;
        else
            bgmSource.volume = 0.0f;

        if(bool.Parse(PlayerPrefs.GetString("SFX")))
            sfxSource.volume = 1.0f;
        else
            sfxSource.volume = 0.0f;
            
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
