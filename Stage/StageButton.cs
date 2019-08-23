using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageButton : MonoBehaviour
{   

    private bool isClear;

    public bool IsClear{get => isClear; set => isClear = value;}
    private bool isUnlock;
    
    public bool IsUnlock{get => isUnlock;}
    [SerializeField]
    private StageButton nextStage;

    [SerializeField]
    private int roundNumber;
    [SerializeField]
    private int stageNumber;
    private bool isStar;
    public bool IsStar{get => isStar;}
    

    private void Start(){
        if(PlayerPrefs.HasKey("Round_0" + roundNumber.ToString() + "_" + stageNumber.ToString())){
            IsClear = bool.Parse(PlayerPrefs.GetString("Round_0" + roundNumber.ToString() + "_" + stageNumber));
                
        }else{
            PlayerPrefs.SetString("Round_0" + roundNumber.ToString() + "_" + stageNumber.ToString(),"false");            
        }
        
        if(PlayerPrefs.HasKey("Round_0" + roundNumber.ToString() + "_" + stageNumber.ToString() + "_Star")){
            isStar = bool.Parse(PlayerPrefs.GetString("Round_0" + roundNumber.ToString() + "_" + stageNumber.ToString() + "_Star"));   
        }else{
            PlayerPrefs.SetString("Round_0" + roundNumber.ToString() + "_" + stageNumber.ToString() + "_Star","false");
        }

    }

    public void Unlock(){
        isUnlock = true;
    }
    


}
