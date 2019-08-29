﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
public class StageManager : MonoBehaviour
{

    #region FIELDS
    public static StageManager instance;
    private MapPhasing phasing;
    [SerializeField]

    [BoxGroup("Background")]
    private Image backgroundImage;

    [BoxGroup("Background")]
    [SerializeField]
    private Image themeAdditionImage;

    [BoxGroup("Background")]
    [SerializeField]
    private Image themeColorImage;
    
    [BoxGroup("Background")]
    [SerializeField]
    private Image blackImage;


    [Space(10)]
    [BoxGroup("Fields")]
    [SerializeField]
    private Text limitText;

    [BoxGroup("Fields")]
    [SerializeField]
    private Text missionText;

    [BoxGroup("Fields")]
    [SerializeField]
    private Sprite[] themeBackgrounds;

    [BoxGroup("Fields")]
    [SerializeField]
    private Color[] themeColor;

    [BoxGroup("Fields")]
    [SerializeField]
    private Sprite[] themeAddition;

    #endregion FIELDS


    #region GAME_SETTING
    [Space(10)]
    [Header("Game Information Setting")]
    [Space(10)]

    [Dropdown("limitTypes")]
    [SerializeField]
    private string limitType;

    [Dropdown("missionTypes")]
    [SerializeField]
    private string missionType;
    private string stageType; // Original, custom
    private string[] missionTypes = {"Button_Excute", "Time", "MoveCount", "None"};
    private string[] limitTypes = {"Time", "MoveCount", "None"};
    
    private int theme;


    [Space(20)]
    [MinValue(0f),MaxValue(100f)]
    [SerializeField]
    private int limitValue;

    [MinValue(0f),MaxValue(100f)]
    [SerializeField]
    private int missionValue;
    private float floatTimer;
    private int moveCount = 0;
    private bool isLimitTypeMoveCount = false;
    private bool isMissionTypeMoveCount = false;

    [SerializeField]
    private bool missionClear;
    public int MoveCount {
        get => moveCount; 
        set {
                moveCount = value;
                if(isLimitTypeMoveCount)
                    limitText.text = moveCount.ToString();
                if(isLimitTypeMoveCount && moveCount <= 0)
                    GameEnd();

                if(isMissionTypeMoveCount)
                    missionText.text = moveCount.ToString();
                if(isMissionTypeMoveCount && moveCount <= 0)
                    missionClear = false;
            }
        }

    #endregion GAME_SETTING
    private void Awake(){
        if(instance == null)
            instance = this;
    }

    public void GameSetting(int stageNumber, string stageType, string limitType, int limitValue, string missionType, int missionValue, int theme){
        this.stageType = stageType;
        this.limitType = limitType;
        this.limitValue = limitValue;
        this.missionType = missionType;
        this.missionValue = missionValue;
        this.theme = theme;
    }

    private void Start() {



        GameObject[] sides = GameObject.FindGameObjectsWithTag("Side");
        phasing = gameObject.GetComponent<MapPhasing>() ?? null;
        phasing?.Phasing();
        for(int i = 0; i < 6; i++){
            sides[i].GetComponent<MeshRenderer>().material = GetResoueceMaterials(sides[i].GetComponent<CubeColor>().SideColor);
        }

        if(!stageType.Equals("Custom")){
            backgroundImage.sprite = Resources.Load<Sprite>("StageObject/" + GameManager.instance.nextRound + "/Background");
            themeAdditionImage.sprite = Resources.Load<Sprite>("StageObject/" + GameManager.instance.nextRound + "/Addition");
            themeColorImage.color = themeColor[theme];
        }else{
            backgroundImage.sprite = themeBackgrounds[theme];
            themeAdditionImage.sprite = themeAddition[theme];
            themeColorImage.color = themeColor[theme];
        }

        StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));


        if(limitType.Equals("Time")){
            floatTimer = (float)limitValue;
            StartCoroutine(GameTimer());
        }    
        else if(limitType.Equals("MoveCount")){
            isLimitTypeMoveCount = true;
            moveCount = limitValue;
        }

        if(missionType.Equals("Time")){
            missionClear = true;
            StartCoroutine(GameTimer());                
        }
        else if(missionType.Equals("MoveCount")){
            missionClear = true;
            isMissionTypeMoveCount = true;
            moveCount = missionValue;
        }
        else if(missionType.Equals("Button_Excute")){
        }

        StartCoroutine(ImageFadeInOut());
    }

    private IEnumerator ImageFadeInOut(){
        
        WaitForSeconds waitingTime = new WaitForSeconds(0.1f);
        WaitForSeconds repeatDelay = new WaitForSeconds(2.5f);
        while(true){
            for(int i = 0; i < 10; i++){
                themeColorImage.color = new Color(themeColorImage.color.r, themeColorImage.color.g, themeColorImage.color.b, themeColorImage.color.a - 0.025f);
                themeAdditionImage.color = new Color(themeAdditionImage.color.r,themeAdditionImage.color.g,themeAdditionImage.color.b,themeAdditionImage.color.a - 0.1f);
                yield return waitingTime;
            }   

            for(int i = 0; i < 10; i++){
                themeColorImage.color = new Color(themeColorImage.color.r, themeColorImage.color.g, themeColorImage.color.b, themeColorImage.color.a + 0.025f);
                themeAdditionImage.color = new Color(themeAdditionImage.color.r,themeAdditionImage.color.g,themeAdditionImage.color.b,themeAdditionImage.color.a + 0.1f);
                yield return waitingTime;
            }     

            yield return repeatDelay;
        }
    }

    private IEnumerator GameTimer(){
        var waitingTime = new WaitForSecondsRealtime(0.02f);
        
        while(true){
            floatTimer -= Time.deltaTime;
            
            if(limitType.Equals("Time"))
                limitText.text = floatTimer.ToString("N2");

            else if(missionType.Equals("Time"))
                missionText.text = floatTimer.ToString("N2");

            if(floatTimer < 0){
                
                if(limitType.Equals("Time"))
                    GameEnd();
                else if(missionType.Equals("Time"))
                    missionClear = false;

                break;
            }
            
            yield return waitingTime;
        }
    }

    private Material GetResoueceMaterials(int index){
        switch(index){
            case 0:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Lime"); 
  
            case 1:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Blue"); 
   
            case 2:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Red"); 
                
            case 3:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Magenta"); 

            case 4:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Orange"); 

            case 5:
            return Resources.Load<Material>("StageObject/" + GameManager.instance.nextRound + "/Cube/Materials/" + "Yellow"); 

        }
        return null;
    }


    [Button("GameEnd")]
    public void GameEnd(){
        StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.2f));
        if(!bool.Parse(PlayerPrefs.GetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star"))){
            PlayerPrefs.SetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star","false");   
        } 

        
    }
    public void Retry(){
        SceneManager.LoadScene("02.InGame");
    }
    [Button("GameClear")]    
    public void GameClear(){
        
        if(!bool.Parse(PlayerPrefs.GetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star")) && missionClear){
            GameManager.instance.Star += GetMapStar();
            PlayerPrefs.SetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString() + "_Star","true");   
        }

        PlayerPrefs.SetString(GameManager.instance.nextRound + "_" + GameManager.instance.nextStageNumber.ToString(),"true");
    
    }

    private int GetMapStar(){
        
        return 1;
    }
}
