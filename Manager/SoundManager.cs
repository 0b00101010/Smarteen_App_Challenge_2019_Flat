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
            if(bgmOnOff)
                PlayerPrefs.SetString("BGM","true");
            else
                PlayerPrefs.SetString("BGM","false");
            Setting();
        }
    }

    private bool sfxOnOff;

    public bool SFXOnOff{
        get => sfxOnOff;
        set {
            sfxOnOff = value;
            if(sfxOnOff)
                PlayerPrefs.SetString("SFX","true");
            else
                PlayerPrefs.SetString("SFX","false");                
                
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
            sfxOnOff = bool.Parse(PlayerPrefs.GetString("SFX"));
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
