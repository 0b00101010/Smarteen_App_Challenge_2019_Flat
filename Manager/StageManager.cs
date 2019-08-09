using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Image blackImage;

    private void Awake(){
        if(instance == null)
            instance = this;
    }

    private void Start() {
        backgroundImage.sprite = Resources.Load<Sprite>("StageObject/" + GameManager.instance.nextRound + "/Background");
        GameObject[] sides = GameObject.FindGameObjectsWithTag("Side");
        for(int i = 0; i < 6; i++){
            sides[i].GetComponent<MeshRenderer>().material = GetResoueceMaterials(sides[i].GetComponent<CubeColor>().SideColor);
        }

        // StartCoroutine(GameManager.instance.IFadeOut(blackImage,0.5f));
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
        // StartCoroutine(GameManager.instance.IFadeIn(blackImage,0.5f));
    }
}
