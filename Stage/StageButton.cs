using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageButton : MonoBehaviour
{   

    [SerializeField]
    private bool isClear;

    public bool IsClear{get => isClear; set => isClear = value;}
    [SerializeField]
    private bool isUnlock;
    private Button thisButton;    
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

        thisButton = gameObject.GetComponent<Button>();

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

        if(isUnlock)
            thisButton.interactable = true;
        else
            thisButton.interactable = false;

        if(isClear)
            nextStage?.Unlock();

        //Image Change

        if(isUnlock){
            if(isStar){
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Clear");
            }
            else{
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/UnLock");
            }
        }else {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Lock");
        }
    }

    public void Clear(){
        PlayerPrefs.SetString("Round_0" + roundNumber.ToString() + "_" + stageNumber.ToString(),"true"); 
        nextStage.Unlock();     
    }

    public void Unlock(){
        isUnlock = true;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/UnLock");
        thisButton.interactable = true;
    }
}
