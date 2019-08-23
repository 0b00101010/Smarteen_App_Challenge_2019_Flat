using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private LoadingBar loadingBar;

    private float loadingAmount;
    private int sceneNumber = 1;
    public float LoadingAmount{
        get => loadingAmount;
        set {
            loadingAmount = value;
            if(loadingAmount == 1.0f)
                StartCoroutine(NextSceneLoad());
                
        }
    }
    [SerializeField]
    private Image blackBackground;

    private void Start(){
        loadingBar.LoadingStart();
    }

    private void Update(){
        LoadingAmount = loadingBar.LoadingBarAmount;
    }


    private IEnumerator NextSceneLoad(){
        yield return StartCoroutine(GameManager.instance.IFadeIn(blackBackground,0.5f,30));
        SceneManager.LoadScene("02.InGame");
    }
}
