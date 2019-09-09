using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private LoadingBar loadingBar;
    private AsyncOperation asyncOperation;
    private float loadingAmount;
    private int sceneNumber = 1;
    public float LoadingAmount{
        get => loadingAmount;
        set {
            loadingAmount = value;
            if(loadingAmount >= 0.9f && loadingBar.LoadingBarAmount == 1.0f)
                StartCoroutine(NextSceneLoad());
                
        }
    }
    

    [SerializeField]
    private Image blackBackground;

    private void Start(){
        asyncOperation = SceneManager.LoadSceneAsync("02.InGame");
        asyncOperation.allowSceneActivation = false;
        loadingBar.LoadingStart();

    }

    private void Update(){
        LoadingAmount = asyncOperation.progress;
    }


    private IEnumerator NextSceneLoad(){
        Debug.Log("asdf");
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackBackground,0.5f,30));
        asyncOperation.allowSceneActivation = true;
    }
}
