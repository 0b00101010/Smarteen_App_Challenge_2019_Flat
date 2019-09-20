﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager instance;

    [SerializeField]
    private Image blackImage;
    [SerializeField]
    private Image creditImage;

    [SerializeField]
    private Canvas settingCanvas;
    private Ray ray = new Ray();
    [SerializeField]
    private Button bgmButton;
    [SerializeField]
    private Button sfxButton;
    [SerializeField]
    private Button languageButton;
    private bool creditOn = false;
    [SerializeField]
    private Sprite[] soundSprite;
    private void Start(){
        if(instance == null)
            instance = this;

        StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
        //StartCoroutine(WhiteFadeInOut());
        if(GameManager.instance.soundManager.BGMOnOff)
            bgmButton.image.sprite = soundSprite[0];
            
        else             
            bgmButton.image.sprite = soundSprite[1];
        
        if(GameManager.instance.soundManager.SFXOnOff)
            sfxButton.image.sprite = soundSprite[0];      
            
        else
            sfxButton.image.sprite = soundSprite[1];            

        //if(GameManager.instance.LaguageCord.Equals(0))
        //else   
    }
    private void Update(){
        if(blackImage.color.a < 0.005f)
            blackImage.gameObject.SetActive(false);

        if((settingCanvas.gameObject.activeInHierarchy ||  creditOn) && (GameManager.instance.touchManager.IsTouch || Input.GetMouseButtonDown(0))){
            CastRay();
        }

   
        // #endif

        // #if UNITY_ANDROID
       
        
    }

    private void CastRay(){
        
        // ray.origin = GameManager.instance.touchManager.GetPosition();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // ray.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ray.direction = Vector3.forward;
        // RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction,Mathf.Infinity,LayerMask.GetMask("UI"));
        
        // if(hit.collider == null){
        //     settingCanvas.gameObject.SetActive(false);
        // }
        
        RaycastHit hit; 

        if(!creditOn){
            if(Physics.Raycast(ray,out hit,Mathf.Infinity,LayerMask.GetMask("UI"))){
                if(hit.collider == null)
                    settingCanvas.gameObject.SetActive(false);
            }
            else{
                settingCanvas.gameObject.SetActive(false);
            }
        }
        else{
            if(Physics.Raycast(ray,out hit,Mathf.Infinity,LayerMask.GetMask("UI"))){
                if(hit.collider == null)
                    creditImage.gameObject.SetActive(false);
            }
            else{
                creditImage.gameObject.SetActive(false);
            }
        }
    }

    public void StageSelectScene(){
        // Debug.Log("SceneLoad");
        blackImage.gameObject.SetActive(true);
        StartCoroutine(StageSelectSceneLoad());

    }

    private IEnumerator StageSelectSceneLoad(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.5f));
        SceneManager.LoadScene("01.StageSelectScene");
    }

    public void CustomScene(){
        blackImage.gameObject.SetActive(true);
        StartCoroutine(CustomSceneLoad());
    }

    private IEnumerator CustomSceneLoad(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.5f));
        SceneManager.LoadScene("03.CustomeScene");
    }

    public void SettingButton(){
        settingCanvas.gameObject.SetActive(true);
    }

    public void BgmOnOff(){
        
        GameManager.instance.soundManager.BGMOnOff = !GameManager.instance.soundManager.BGMOnOff;
        
        if(GameManager.instance.soundManager.BGMOnOff)
            bgmButton.image.sprite = soundSprite[0];
            
        else             
            bgmButton.image.sprite = soundSprite[1];

    }

    public void SfxOnOff(){
        
        GameManager.instance.soundManager.SFXOnOff = !GameManager.instance.soundManager.SFXOnOff;
        
        if(GameManager.instance.soundManager.SFXOnOff)
            sfxButton.image.sprite = soundSprite[0];            
        else 
            sfxButton.image.sprite = soundSprite[1];
        
    }

    public void LanguageChange(){


        if(GameManager.instance.LaguageCord.Equals(0)){
            GameManager.instance.LaguageCord = 1;

            // Button Setting
        }
        else{ 
            GameManager.instance.LaguageCord = 0;

            // Button Setting
        }
    }

    public void Credit(){
        creditImage.gameObject.SetActive(true);
        creditOn = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
