using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    private MapPhasing phasing;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Image blackImage;

    [SerializeField]
    private Text textTimer;
    private int moveCount = 0;
    private bool isMissionClear = false;

    private float floatTimer = 30;
    private void Awake(){
        if(instance == null)
            instance = this;
    }

    private void Start() {
        backgroundImage.sprite = Resources.Load<Sprite>("StageObject/" + GameManager.instance.nextRound + "/Background");
        GameObject[] sides = GameObject.FindGameObjectsWithTag("Side");
        phasing = gameObject.GetComponent<MapPhasing>();
        phasing.Phasing();
        for(int i = 0; i < 6; i++){
            sides[i].GetComponent<MeshRenderer>().material = GetResoueceMaterials(sides[i].GetComponent<CubeColor>().SideColor);
        }

        StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
    }

    private void FixedUpdate(){
        floatTimer -= Time.deltaTime;
        textTimer.text = floatTimer.ToString("N2");

        if(floatTimer < 0){
            GameEnd();
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

        return 0;
    }
}
