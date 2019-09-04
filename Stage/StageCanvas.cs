using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageCanvas : MonoBehaviour{

    [SerializeField]
    private Canvas stageButtonCanvas;
    
    [SerializeField]
    private StageSelectSceneManager sceneManager;

    
    [SerializeField]
    private string roundName;
    private Ray ray = new Ray();

    private StageButton[] stageButtons;
    
    [SerializeField]
    private StageButton nextStageButton;

    private int getStar;
    private int maxStar;

    private void Start(){
        stageButtons = stageButtonCanvas?.GetComponentsInChildren<StageButton>();
        maxStar = stageButtons.Length;

        for(int i = 0; i < stageButtons.Length; i++){
            if(stageButtons[i].IsStar)
                getStar++;
        }

        if(getStar == stageButtons.Length)
            nextStageButton?.Unlock();

    }
    public void LoadScene(int nextStageNumber){
        GameManager.instance.nextRound = roundName;
        GameManager.instance.nextStageNumber = nextStageNumber;
        StartCoroutine(LoadSceneCoroutine());
    }

    public IEnumerator LoadSceneCoroutine(){
        yield return StartCoroutine(sceneManager.FadeIn());
        SceneManager.LoadScene("LoadingScene");   
    }
}