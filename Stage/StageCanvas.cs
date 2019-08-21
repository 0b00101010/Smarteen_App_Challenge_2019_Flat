using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCanvas : MonoBehaviour{

    [SerializeField]
    private Canvas stageButtonCanvas;
    
    [SerializeField]
    private Canvas stageCanvas;

    [SerializeField]
    private Text getStarText;
    
    private StageButton[] stageButtons;
    private int getStar;
    private int maxStar;

    private void Start(){
        stageButtons = stageButtonCanvas.GetComponentsInChildren<StageButton>();

        maxStar = stageButtons.Length;

        for(int i = 0; i < stageButtons.Length; i++){
            if(stageButtons[i].IsStar)
                getStar++;
        }


        getStarText.text = getStar.ToString() + " / " + maxStar.ToString();
    }

    public void OpenButtons(){
        stageCanvas.enabled = false;
        stageButtonCanvas.enabled = true;
    } 

    public void CloseButtons(){
        stageCanvas.enabled = true;
        stageButtonCanvas.enabled = false;
    }
    
}