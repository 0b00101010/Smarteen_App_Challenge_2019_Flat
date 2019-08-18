using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;
    
    [SerializeField]
    private Image cubeImage;

    [SerializeField]
    private Image loadingText;
    [ProgressBar("LoadingBar",100,ProgressBarColor.Indigo)]
    private float loadingBarAmount;
    
    public float LoadingBarAmount{get => loadingBarAmount;}

    private Vector3 cubePosition;
    private Vector3 smallerScale = new Vector3(0.0025f,0.0025f,0.0025f);
    private Vector3 originalScale = new Vector3(1.0f,1.0f,1.0f);
    private float rotateAngle = 0.0f;
    private IEnumerator loadingCoroutine;

    private void Awake() {
        loadingCoroutine = Loading();
        cubePosition = cubeImage.transform.position;
    }

    [Button("Loading")]
    public void LoadingStart(){
        LoadingStop();
        StartCoroutine(loadingCoroutine);
    }

    [Button("StopLoading")]
    private void LoadingStop(){
        StopCoroutine(loadingCoroutine);
        ResetLoadingUI();
    }

    private void ResetLoadingUI(){
        cubeImage.rectTransform.position = cubePosition;
        cubeImage.rectTransform.rotation = Quaternion.Euler(0,0,0);
        cubeImage.rectTransform.localScale = originalScale;
        rotateAngle = 0;
        loadingBar.fillAmount = 0.0f;
    }

    

    private IEnumerator Loading(){
        ResetLoadingUI();
        
        while(true){
            if(loadingBar.fillAmount < 0.9f)
                loadingBar.fillAmount += 0.005f;
            else 
                loadingBar.fillAmount = 0.0f;

            loadingBarAmount = loadingBar.fillAmount;

            if(rotateAngle == -450){
                StartCoroutine(CubeFadeInOut());
            }else if(rotateAngle > -450){
                cubeImage.rectTransform.rotation = Quaternion.Euler(0,0,rotateAngle);
                cubeImage.rectTransform.position = new Vector2(cubeImage.rectTransform.position.x + 1.2f,cubeImage.rectTransform.position.y);
                cubeImage.rectTransform.localScale -= smallerScale;
            }

            rotateAngle -= 2 ;
            
            yield return null;
        }

    }

    private IEnumerator CubeFadeInOut(){
        yield return StartCoroutine(GameManager.instance.IFadeOut(cubeImage,0.25f));
        cubeImage.transform.position = cubePosition;
        cubeImage.rectTransform.localScale = new Vector3(1.0f,1.0f,1.0f);
        yield return StartCoroutine(GameManager.instance.IFadeIn(cubeImage,0.25f));
        rotateAngle = 0;
    }

}
