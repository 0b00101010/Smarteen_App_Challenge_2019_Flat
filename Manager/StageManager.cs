using System.Collections;
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

    [BoxGroup("Fields")]
    private Image backgroundImage;

    [BoxGroup("Fields")]
    [SerializeField]
    private Image blackImage;

    [BoxGroup("Fields")]
    [SerializeField]
    private Text textTimer;
    private int moveCount = 0;
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
    
    private string theme;

    private int limitValue;
    private int missionValue;

    [ShowIf("LimitTypeTime")]
    [MinValue(30f),MaxValue(100f)]
    [SerializeField]
    private float floatTimer = 1000;
    #endregion GAME_SETTING
    private void Awake(){
        if(instance == null)
            instance = this;
    }

    private bool LimitTypeTime(){
        if(limitType.Equals("Time"))
            return true;

        return false;
    }

    private bool LimitTypeMoveCount(){
        if(limitType.Equals("MoveCount"))
            return true;
        return false;
    }

    public void GameSetting(int stageNumber, string stageType, string limitType, int limitMissionValue, string missionType, int missionValue, string theme){
        this.stageType = stageType;
        this.limitType = limitType;
        this.limitValue = limitMissionValue;
        this.missionType = missionType;
        this.missionValue = missionValue;
        this.theme = theme;
    }

    private void Start() {

        backgroundImage.sprite = Resources.Load<Sprite>("StageObject/" + GameManager.instance.nextRound + "/Background");
        GameObject[] sides = GameObject.FindGameObjectsWithTag("Side");
        //phasing = gameObject.GetComponent<MapPhasing>();
        //phasing.Phasing();
        for(int i = 0; i < 6; i++){
            sides[i].GetComponent<MeshRenderer>().material = GetResoueceMaterials(sides[i].GetComponent<CubeColor>().SideColor);
        }

        StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
    }

    private IEnumerator GameTimer(){
        var waitingTime = new WaitForSecondsRealtime(0.02f);
        
        while(true){
            floatTimer -= Time.deltaTime;
            textTimer.text = floatTimer.ToString("N2");

            if(floatTimer < 0){
                GameEnd();
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
        PlayerPrefs.SetString("Stage_0" + GameManager.instance.nextStageNumber,"UnClear");
    }
    public void Retry(){
        SceneManager.LoadScene("02.InGame");
    }
    
    public void GameClear(){
        if(PlayerPrefs.HasKey("Stage_0" + GameManager.instance.nextStageNumber)){
            if(!PlayerPrefs.GetString("Stage_0" + GameManager.instance.nextStageNumber).Equals("Clear"))
                GameManager.instance.Star += GetMapStar();
        }else{
            GameManager.instance.Star += GetMapStar();
            PlayerPrefs.SetString("Stage_0" + GameManager.instance.nextStageNumber,"Clear");
        }
    }

    private int GetMapStar(){
        return 1;
    }
}
