using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using NaughtyAttributes;
public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;
    
    [SerializeField]
    private Image cubeImage;

    [SerializeField]
    private Image blackBackground;

    [SerializeField]
    private Image loadingText;
    [ProgressBar("LoadingBar",100,ProgressBarColor.Indigo)]
    private float loadingBarAmount;
    
    [SerializeField]
    private Sprite[] tipsKor;

    [SerializeField]
    private Sprite[] tipsEng;

    [SerializeField]
    private Image tipImage; 

    public float LoadingBarAmount{get => loadingBarAmount;}

    private Vector3 cubePosition;
    private Vector3 smallerScale = new Vector3(0.000f,0.000f,0.000f);
    private Vector3 originalScale = new Vector3(1.0f,1.0f,1.0f);
    private float rotateAngle = 0.0f;
    private IEnumerator loadingCoroutine;
    private AsyncOperation asyncOperation;
    private float loadingTimer = 0.0f;

    private void Start() {
        loadingCoroutine = Loading();
        cubePosition = cubeImage.transform.position;
        LoadingStart();

        if(GameManager.instance.LanguageCord.Equals(0))
            tipImage.sprite = tipsKor[Random.Range(0,tipsKor.Length)];
        else if(GameManager.instance.LanguageCord.Equals(1))
            tipImage.sprite = tipsEng[Random.Range(0,tipsEng.Length)];
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
        tipImage.fillAmount = 0.0f;
    }
    

    private IEnumerator Loading(){
        ResetLoadingUI();

        asyncOperation = SceneManager.LoadSceneAsync("02.InGame");
        asyncOperation.allowSceneActivation = false;

        Time.timeScale = 1.0f;
        if(asyncOperation == null)
            Debug.Log("SceneLoadingManager :: Error, asyncOpearation index null");


        while(!asyncOperation.isDone){
            
            loadingTimer += Time.deltaTime;

            if(tipImage.fillAmount < 1.0f)
                tipImage.fillAmount += 0.1f;

            if(asyncOperation.progress >= 0.9f){
                loadingBar.fillAmount = 1;
                
                if(rotateAngle == -360){
                    StartCoroutine(GameManager.instance.IFadeIn(blackBackground,0.1f));
                    asyncOperation.allowSceneActivation = true;
                }
            }
            else{
                loadingBar.fillAmount = Mathf.Lerp(0, 1, asyncOperation.progress);
            }

            // loadingBar.fillAmount = asyncOperation.progress;

            // if(loadingBar.fillAmount >= 1.0f)
            //     asyncOperation.allowSceneActivation = true;

            if(rotateAngle == -360){
                StartCoroutine(CubeFadeInOut());
                cubeImage.rectTransform.rotation = Quaternion.Euler(0,0,0);
            }else if(rotateAngle > -360){
                cubeImage.rectTransform.rotation = Quaternion.Euler(0,0,rotateAngle);
                cubeImage.rectTransform.Translate(new Vector2(0.9f,0),Space.World);            
                cubeImage.rectTransform.localScale -= smallerScale;
            }
            
            rotateAngle -= 3 ;
            
            
            yield return null;
        }

    }

    private IEnumerator CubeFadeInOut(){
        yield return StartCoroutine(GameManager.instance.IFadeOut(cubeImage,0.25f));
        cubeImage.transform.position = cubePosition;
        cubeImage.rectTransform.rotation = Quaternion.Euler(0,0,0);
        cubeImage.rectTransform.localScale = new Vector3(1.0f,1.0f,1.0f);
        yield return StartCoroutine(GameManager.instance.IFadeIn(cubeImage,0.25f));
        
        rotateAngle = 0;
    }

}
